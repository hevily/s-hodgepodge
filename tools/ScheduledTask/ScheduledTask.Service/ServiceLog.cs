using System;
using System.IO;
using System.Text;

namespace ScheduledTask.Service
{
    /// <summary>
    /// 服务的日志
    /// </summary>
    public static class ServiceLog
    {
        static ServiceLog()
        {
            //获取是否记录调试信息
            string tmpConfig = System.Configuration.ConfigurationManager.AppSettings["RecordDebugLog"];
            if (tmpConfig != null)
            {
                tmpConfig = tmpConfig.Trim().ToLower();
                RecordDebugLog = (tmpConfig == "1" || tmpConfig == "true");
            }
        }

        /// <summary>
        /// 用于线程互斥的对象
        /// </summary>
        private static readonly object LockObj = new object();
        /// <summary>
        /// 是否记录调试信息
        /// </summary>
        private static readonly bool RecordDebugLog = false;

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="msg"></param>
        public static void SaveLog(string msg)
        {
            string filePath = RuntimeInfo.ApplicationFolder;
            filePath += "ScheduledTaskLog\\";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            filePath = filePath + DateTime.Now.ToString("yyyyMMdd") + "_log.txt";
            lock (LockObj)
            {
                System.IO.File.AppendAllText(filePath, msg + "\r\n", System.Text.Encoding.UTF8);
            }
        }

        /// <summary>
        /// 保存调试信息
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="msg"></param>
        public static void SaveDebugLog(string taskName, string msg)
        {
            if (RecordDebugLog)
            {
                string filePath = RuntimeInfo.ApplicationFolder;
                filePath += "ScheduledDebugLog\\";
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                filePath = filePath + DateTime.Now.ToString("yyyyMMdd") + "_log.txt";
                StringBuilder logBuffer = new StringBuilder();
                logBuffer.Append("[").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("]");
                logBuffer.Append("[").Append(taskName).AppendLine("]");
                logBuffer.AppendLine(msg);
                lock (LockObj)
                {
                    System.IO.File.AppendAllText(filePath, logBuffer.ToString(), System.Text.Encoding.UTF8);
                }
            }
        }
    }
}
