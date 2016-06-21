using System;

namespace ScheduledTask.Entity
{
    /// <summary>
    /// 任务执行结果
    /// </summary>
    [Serializable]
    public class TaskExecuteResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get;
            set;
        }

        /// <summary>
        /// 执行结果的描述
        /// </summary>
        public string ResultDescription
        {
            get;
            set;
        }

        /// <summary>
        /// 执行失败时相关的异常
        /// </summary>
        public Exception AboutException
        {
            get;
            set;
        }

        /// <summary>
        /// 失败后是否跳过重试
        /// </summary>
        public bool SkipRetryAfterFailure
        {
            get;
            set;
        }
    }
}
