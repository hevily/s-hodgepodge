using ScheduledTask.Entity;

namespace ScheduledTask.Interface
{
    /// <summary>
    /// 任务的接口
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// 初始化，提取相关的配置信息
        /// (建议每个任务设置自己的配置信息，存在到自己的XML文件中，
        /// 相关文件存放于“/Config/Tasks”目录中)
        /// </summary>
        /// <param name="applicationFolder">程序所在目录</param>
        /// <returns></returns>
        TaskConfiguration Init(string applicationFolder);

        /// <summary>
        /// 确认指定的任务轮次是否已经完成，用于服务启动后首次任务的确认
        /// （对TaskLoopType.PerMonth、TaskLoopType.PerWeek、
        /// TaskLoopType.PerDay、TaskLoopType.PerHour有效）
        /// </summary>
        /// <param name="config">任务相关的配置</param>
        /// <param name="theActionTime">任务轮次，格式为"yyyy-MM-dd HH:mm"</param>
        /// <returns></returns>
        bool CheckTaskHaveRun(TaskConfiguration config, string theActionTime);

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="applicationFolder">程序所在目录</param>
        /// <param name="theActionTime">任务轮次，格式为"yyyy-MM-dd HH:mm"</param>
        /// <returns>任务的执行结果</returns>
        TaskExecuteResult ExecuteTask(string applicationFolder, string theActionTime);
    }
}
