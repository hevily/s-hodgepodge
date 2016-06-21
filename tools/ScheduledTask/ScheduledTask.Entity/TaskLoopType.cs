namespace ScheduledTask.Entity
{
    /// <summary>
    /// 计划任务的重复模式
    /// </summary>
    public enum TaskLoopType
    {
        /// <summary>
        /// 每个月一次
        /// </summary>
        PerMonth = 1,
        /// <summary>
        /// 每周一次
        /// </summary>
        PerWeek = 2,
        /// <summary>
        /// 每天一次
        /// </summary>
        PerDay = 3,
        /// <summary>
        /// 每小时一次
        /// </summary>
        PerHour = 4,
        /// <summary>
        /// 间隔指定的秒数
        /// </summary>
        SecondInterval = 5,
        /// <summary>
        /// 不循环
        /// </summary>
        NotLoop = 16
    }
}
