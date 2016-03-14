using System;

namespace IISJournalAnalyzer
{
    /// <summary>
    /// 日志节点
    /// </summary>
    public class LogItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logLine"></param>
        /// <returns></returns>
        public bool LoadData(string logLine)
        {
            if (string.IsNullOrEmpty(logLine))
                return false;

            logLine = logLine.Trim();
            if (logLine.StartsWith("#"))
                return false;

            try
            {
                this.TheLogLine = logLine;

                int index0 = -1, index1 = -1;
                index0 = logLine.IndexOf(" ");
                index1 = logLine.IndexOf(" ", index0 + 1);
                this.RequestTime = DateTime.Parse(logLine.Substring(0,index1));

                index0 = logLine.IndexOf(" ", index1 + 1);
                index1 = logLine.IndexOf(" ", index0 + 1);
                this.ServerIP = logLine.Substring(index0 + 1, index1 - index0 - 1);

                index0 = index1;
                index1 = logLine.IndexOf(" ", index0 + 1);
                this.RequestMethod = logLine.Substring(index0 + 1, index1 - index0 - 1);

                index0 = index1;
                index1 = logLine.IndexOf(" ", index0 + 1);
                this.RequestUrl = logLine.Substring(index0 + 1, index1 - index0 - 1);

                if (logLine[index1 + 1] == '-')//无参数
                {
                    index0 = logLine.IndexOf("-", index1 + 2) + 1;//端口之后的空格
                }
                else
                {
                    index0 = index1;
                    index1 = logLine.IndexOf(" ", index0 + 1);
                    this.UrlParams = logLine.Substring(index0 + 1, index1 - index0 - 1);

                    index0 = logLine.IndexOf("-", index1) + 1;//端口之后的空格
                }

                index1 = logLine.IndexOf(" ", index0 + 1);
                this.ClientIP = logLine.Substring(index0 + 1, index1 - index0 - 1);

                index0 = index1;
                index1 = logLine.IndexOf(" ", index0 + 1);
                this.UserAgent = logLine.Substring(index0 + 1, index1 - index0 - 1);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 日志行
        /// </summary>
        public string TheLogLine
        {
            get;
            set;
        }

        /// <summary>
        /// 请求的时间
        /// </summary>
        public DateTime RequestTime
        {
            get;
            set;
        }

        /// <summary>
        /// 请求的类型
        /// </summary>
        public string RequestMethod
        {
            get;
            set;
        }

        /// <summary>
        /// 请求的地址
        /// </summary>
        public string RequestUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        public string UrlParams
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP
        {
            get;
            set;
        }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIP
        {
            get;
            set;
        }

        /// <summary>
        /// 客户端信息
        /// </summary>
        public string UserAgent
        {
            get;
            set;
        }
    }
}
