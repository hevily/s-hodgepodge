using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using ScheduledTask.Entity;
using ScheduledTask.Interface;

namespace ScheduledTask.Service
{
    /// <summary>
    /// 定时任务轮询器
    /// </summary>
    public class TaskPolling
    {
        #region 参数区

        /// <summary>
        /// 任务信息列表
        /// </summary>
        private static readonly List<TaskInfo> TaskInfoList = new List<TaskInfo>(16);

        #endregion

        /// <summary>
        /// 初始化任务信息
        /// </summary>
        public static void InitTaskInfos()
        {
            //加载定时任务
            string strTaskAssembly = System.Configuration.ConfigurationManager.AppSettings["OATaskAssemblys"];
            string[] taskAssemblys = strTaskAssembly.Split('|');
            for (int t = 0; t < taskAssemblys.Length; t++)
            {
                try
                {
                    string[] items = taskAssemblys[t].Split(',');
                    TaskInfo loopInfo = new TaskInfo();
                    loopInfo.FolderOfTask = RuntimeInfo.ApplicationFolder;
                    if (!loopInfo.FolderOfTask.EndsWith(@"\"))
                        loopInfo.FolderOfTask += @"\";
                    AppDomain loopDomain = AppDomain.CreateDomain(items[0], null, new AppDomainSetup()
                    {
                        ApplicationName = items[0],
                        ApplicationBase = loopInfo.FolderOfTask + items[0],
                        ConfigurationFile = items[0] + ".config"
                    });
                    loopInfo.FolderOfTask = loopInfo.FolderOfTask + items[0] + @"\";
                    string assemblyPath = loopInfo.FolderOfTask + items[1];
                    string taskClassName = items[2];
                    loopInfo.DomainToRun = loopDomain;
                    loopInfo.TaskInstance = (ITask)loopDomain.CreateInstanceFromAndUnwrap(assemblyPath, taskClassName);
                    loopInfo.ConfigOfTask = loopInfo.TaskInstance.Init(loopInfo.FolderOfTask);
                    loopInfo.TaskName = loopInfo.ConfigOfTask.TaskName;
                    TaskInfoList.Add(loopInfo);
                }
                catch (Exception ex)
                {
                    ServiceLog.SaveLog("获取定时任务配置：" + taskAssemblys[t] + "的初始化任务信息时失败:" + ex.Message );
                }
            }
        }

        /// <summary>
        /// 轮询线程的入口
        /// </summary>
        public static void PollingThreadEntrance()
        {
            while (RuntimeInfo.ContinueWork)
            {
                for (int i = 0; i < 60 && RuntimeInfo.ContinueWork; i++)
                    Thread.Sleep(1000);
                //每隔1分钟进行一次调度
                if (RuntimeInfo.ContinueWork)
                {
                    for(int i = 0; i < TaskInfoList.Count;i++)
                    {
                        TaskInfo theTaskInfo = TaskInfoList[i];
                        if (theTaskInfo.IsExecuting)
                            continue;

                        var taskConfig = theTaskInfo.ConfigOfTask;
                        string timeTag = GetTimeTagOfLatestOperation(theTaskInfo);

                        if (string.IsNullOrEmpty(theTaskInfo.TimeTagOfNowOperation))
                            theTaskInfo.TimeTagOfNowOperation = timeTag;

                        StringBuilder logBuffer = new StringBuilder();
                        logBuffer.Append("调度任务：本次计算所得的调度时间为[").Append(timeTag).Append("]，");
                        logBuffer.Append("任务已执行的调度时间为[").Append(theTaskInfo.TimeTagOfNowOperation).AppendLine("]。");

                        bool doJob = false;
                        TimeSpan tsToLastExecute;
                        switch (taskConfig.LoopType)
                        {
                            case TaskLoopType.NotLoop:
                                if (!theTaskInfo.HaveExecuteIfNotLoop)
                                    doJob = true;
                                break;
                            case TaskLoopType.SecondInterval:
                                tsToLastExecute = DateTime.Now.Subtract(theTaskInfo.LastExecuteTime);
                                if (tsToLastExecute.TotalSeconds > taskConfig.LoopInterval)
                                    doJob = true;
                                break;
                            case TaskLoopType.PerDay:
                            case TaskLoopType.PerHour:
                            case TaskLoopType.PerMonth:
                            case TaskLoopType.PerWeek:
                                if (timeTag != theTaskInfo.TimeTagOfNowOperation)
                                {
                                    theTaskInfo.TimeTagOfNowOperation = timeTag;
                                    doJob = true;
                                }
                                else if (!theTaskInfo.IsSuccessOfNowOperation && theTaskInfo.ErrorTimesOfNowOperation < taskConfig.TryTimesIfFailure)
                                {
                                    tsToLastExecute = DateTime.Now.Subtract(theTaskInfo.LastExecuteTime);
                                    if (tsToLastExecute.TotalSeconds > taskConfig.TryIntervalAfterFailure)
                                    {
                                        logBuffer.AppendLine("当前为失败后重试");
                                        doJob = true;
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        if (doJob)
                        {
                            logBuffer.AppendLine("经过计算，本次轮询将执行任务新的轮次");
                            DoJobDelegate del = new DoJobDelegate(DoJob);
                            theTaskInfo.IsExecuting = true;
                            theTaskInfo.ExternOperationObject = del;
                            del.BeginInvoke(theTaskInfo, new AsyncCallback(CallBackOfDoJob), theTaskInfo);
                        }
                        else
                        {
                            logBuffer.AppendLine("本次轮询不执行任务");
                        }

                        ServiceLog.SaveDebugLog(theTaskInfo.TaskName, logBuffer.ToString());
                    }
                }
            }
        }

        #region 异步调用

        /// <summary>
        /// 执行任务的委托
        /// </summary>
        /// <param name="task"></param>
        private delegate  void DoJobDelegate(TaskInfo task);

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="task"></param>
        private static void DoJob(TaskInfo task)
        {
            try
            {
                task.AsyncOperationResult = task.TaskInstance.ExecuteTask(task.FolderOfTask, task.TimeTagOfNowOperation);
            }
            catch(Exception ex)
            {
                task.ErrorTimesOfNowOperation = task.ErrorTimesOfNowOperation + 1;
                //失败执行写入日志
                ServiceLog.SaveLog("执行任务(DoJob)：" + task.TaskName + "出错！" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n");
                return;
            }

            var taskConfig = task.ConfigOfTask;
            if(taskConfig.LoopType == TaskLoopType.NotLoop)
            {
                task.HaveExecuteIfNotLoop = task.AsyncOperationResult.IsSuccess;
            }

            if (task.AsyncOperationResult.IsSuccess)
            {
                task.ErrorTimesOfNowOperation = 0;
                task.IsSuccessOfNowOperation = true;
                //成功执行写入日志
                ServiceLog.SaveLog("执行任务：" + task.TaskName + "成功！" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n");
            }
            else
            {
                task.ErrorTimesOfNowOperation = task.ErrorTimesOfNowOperation + 1;
                //失败执行写入日志
                ServiceLog.SaveLog("执行任务：" + task.TaskName + "失败！" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n");
            }
        }

        /// <summary>
        /// 执行任务的回调函数
        /// </summary>
        /// <param name="result"></param>
        private static void  CallBackOfDoJob(IAsyncResult result)
        {
            TaskInfo task = (TaskInfo)result.AsyncState;
            try
            {
                DoJobDelegate del = (DoJobDelegate)task.ExternOperationObject;
                del.EndInvoke(result);
            }
            catch(Exception ex)
            {
                task.ErrorTimesOfNowOperation = task.ErrorTimesOfNowOperation + 1;
                //失败执行写入日志
                ServiceLog.SaveLog("执行任务(CallBackOfDoJob)：" + task.TaskName + "失败！" + DateTime.Now.ToString() + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n");
            }
            finally
            {
                task.IsExecuting = false;
                task.LastExecuteTime = DateTime.Now;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 求解最新的任务轮次
        /// </summary>
        /// <param name="theTaskInfo"></param>
        /// <returns></returns>
        private static string GetTimeTagOfLatestOperation(TaskInfo theTaskInfo)
        {
            var taskConfig = theTaskInfo.ConfigOfTask;
            var nowTime = DateTime.Now;
            DateTime tmpActionTime = DateTime.Now;
            switch (taskConfig.LoopType)
            {
                case TaskLoopType.NotLoop:
                    tmpActionTime = nowTime;
                    break;
                case TaskLoopType.PerDay:
                    tmpActionTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, taskConfig.ActionHour, taskConfig.ActionMinute, 1);
                    if (nowTime.Hour < taskConfig.ActionHour || (nowTime.Hour == taskConfig.ActionHour && nowTime.Minute < taskConfig.ActionMinute))
                        tmpActionTime = tmpActionTime.AddDays(-1);
                    break;
                case TaskLoopType.PerHour:
                    tmpActionTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, nowTime.Hour, taskConfig.ActionMinute, 1);
                    if (nowTime.Minute < taskConfig.ActionMinute)
                        tmpActionTime = tmpActionTime.AddHours(-1);
                    break;
                case TaskLoopType.PerMonth:
                    tmpActionTime = new DateTime(nowTime.Year, nowTime.Month, taskConfig.ActionDayInMonth, taskConfig.ActionHour, taskConfig.ActionMinute, 1);
                    if (nowTime.Day < taskConfig.ActionDayInMonth 
                        || (nowTime.Day == taskConfig.ActionDayInMonth && nowTime.Hour < taskConfig.ActionHour)
                        || (nowTime.Day == taskConfig.ActionDayInMonth && nowTime.Hour == taskConfig.ActionHour && nowTime.Minute < taskConfig.ActionMinute))
                        tmpActionTime = tmpActionTime.AddMonths(-1);
                    break;
                case TaskLoopType.PerWeek:
                    tmpActionTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, taskConfig.ActionHour, taskConfig.ActionMinute, 1);
                    int theDayOfWeek = (int)nowTime.DayOfWeek;
                    if (theDayOfWeek < taskConfig.ActionDayInWeek)
                        tmpActionTime = tmpActionTime.AddDays((taskConfig.ActionDayInWeek - theDayOfWeek));
                    else if (taskConfig.ActionDayInWeek < theDayOfWeek)
                        tmpActionTime = tmpActionTime.AddDays((7 + taskConfig.ActionDayInWeek - theDayOfWeek));
                    if (theDayOfWeek < taskConfig.ActionDayInWeek
                        || (theDayOfWeek == taskConfig.ActionDayInWeek && nowTime.Hour < taskConfig.ActionHour)
                        || (theDayOfWeek == taskConfig.ActionDayInWeek && nowTime.Hour == taskConfig.ActionHour && nowTime.Minute < taskConfig.ActionMinute))
                        tmpActionTime = tmpActionTime.AddDays(-7);
                    break;
                default:
                    break;
            }
            return tmpActionTime.ToString("yyyy-MM-dd HH:mm");
        }

        #endregion
    }
}
