using System;
using ScheduledTask.Entity;
using ScheduledTask.Interface;

namespace ScheduledTask.Service
{
    /// <summary>
    /// 任务信息
    /// </summary>
    public class TaskInfo
    {
        /// <summary>
        /// 用于运行的应用程序域
        /// </summary>
        public AppDomain DomainToRun
        {
            get;
            set;
        }

        /// <summary>
        /// 任务的运行目录
        /// </summary>
        public string FolderOfTask
        {
            get;
            set;
        }

        /// <summary>
        /// 任务接口的实例
        /// </summary>
        public ITask TaskInstance
        {
            get;
            set;
        }

        /// <summary>
        /// 任务的配置
        /// </summary>
        public TaskConfiguration ConfigOfTask
        {
            get;
            set;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName
        {
            get;
            set;
        }

        /// <summary>
        /// 加载后是否已经确认过上次执行结果
        /// </summary>
        public bool HaveQueryExecuteAfterLoad
        {
            get;
            set;
        }

        /// <summary>
        /// 如果设置为不循环时是否进行了首次执行
        /// </summary>
        public bool HaveExecuteIfNotLoop
        {
            get;
            set;
        }

        /// <summary>
        /// 是否正在执行
        /// </summary>
        public bool IsExecuting
        {
            get;
            set;
        }

        private DateTime _lastExecuteTime = new DateTime(2010, 1, 1, 0, 0, 0);
        /// <summary>
        /// 上次执行时间
        /// </summary>
        public DateTime LastExecuteTime
        {
            get
            {
                return _lastExecuteTime;
            }
            set
            {
                _lastExecuteTime = value;
            }
        }

        /// <summary>
        /// 当前操作轮次的时间标记
        /// </summary>
        public string TimeTagOfNowOperation
        {
            get;
            set;
        }

        /// <summary>
        /// 当前操作轮次的错误次数
        /// </summary>
        public int ErrorTimesOfNowOperation
        {
            get;
            set;
        }

        /// <summary>
        /// 本次操作是否成功
        /// </summary>
        public bool IsSuccessOfNowOperation
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展操作的对象
        /// </summary>
        public object ExternOperationObject
        {
            get;
            set;
        }

        /// <summary>
        /// 异步操作的结果
        /// </summary>
        public TaskExecuteResult AsyncOperationResult
        {
            get;
            set;
        }
    }
}
