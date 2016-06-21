namespace ScheduledTask.Service
{
    /// <summary>
    /// 运行信息
    /// </summary>
    public static class RuntimeInfo
    {
        /// <summary>
        /// 程序所在的目录
        /// </summary>
        public static string ApplicationFolder
        {
            get;
            set;
        }

        private static bool _continueWork = true;
        /// <summary>
        /// 是否继续运行
        /// </summary>
        public static bool ContinueWork
        {
            get
            {
                return _continueWork;
            }
            set
            {
                _continueWork = value;
            }
        }
    }
}
