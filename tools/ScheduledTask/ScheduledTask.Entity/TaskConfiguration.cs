using System;

namespace ScheduledTask.Entity
{
    /// <summary>
    /// 任务的配置
    /// </summary>
    [Serializable]
    public class TaskConfiguration
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName
        {
            get;
            set;
        }

        private TaskLoopType _loopType = TaskLoopType.NotLoop;
        /// <summary>
        /// 任务的循环模式
        /// </summary>
        public TaskLoopType LoopType
        {
            get
            {
                return _loopType;
            }
            set
            {
                _loopType = value;
            }
        }

        /// <summary>
        /// 循环的间隔时间（对TaskLoopType.SecondInterval有效）
        /// </summary>
        public int LoopInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 执行相关操作的时间在月份中的天数（对TaskLoopType.PerMonth有效）
        /// </summary>
        public int ActionDayInMonth
        {
            get;
            set;
        }

        /// <summary>
        /// 执行相关操作的时间在周中的天数（对TaskLoopType.PerWeek有效）
        /// </summary>
        public int ActionDayInWeek
        {
            get;
            set;
        }

        /// <summary>
        /// 执行相关操作的时间（小时部分，对TaskLoopType.PerMonth、TaskLoopType.PerWeek、
        /// TaskLoopType.PerDay有效）
        /// </summary>
        public int ActionHour
        {
            get;
            set;
        }

        /// <summary>
        /// 执行相关操作的时间（分钟部分，对TaskLoopType.PerMonth、TaskLoopType.PerWeek、
        /// TaskLoopType.PerDay、TaskLoopType.PerHour有效）
        /// </summary>
        public int ActionMinute
        {
            get;
            set;
        }

        private int _tryTimesIfFailure = 4;
        /// <summary>
        /// 失败后的尝试次数
        /// </summary>
        public int TryTimesIfFailure
        {
            get
            {
                return _tryTimesIfFailure;
            }
            set
            {
                _tryTimesIfFailure = value;
            }
        }

        private int _tryIntervalAfterFailure = 300;
        /// <summary>
        /// 失败后的尝试时间间隔，单位为秒
        /// </summary>
        public int TryIntervalAfterFailure
        {
            get
            {
                return _tryIntervalAfterFailure;
            }
            set
            {
                _tryIntervalAfterFailure = value;
            }
        }

        /// <summary>
        /// 任务首次执行的时间，格式为"yyyy-MM-dd HH:mm"
        /// </summary>
        private string _fristActionTime = "1900-01-01 00:00";
        public string FristActionTime
        {
            get
            {
                return _fristActionTime;
            }
            set
            {
                _fristActionTime = value;
            }
        }
    }
}
