using System;
using System.Globalization;
using System.ServiceProcess;

namespace ScheduledTask.Service
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            try
            {
                //获取运行路径
                RuntimeInfo.ApplicationFolder = System.AppDomain.CurrentDomain.BaseDirectory;
                if (!RuntimeInfo.ApplicationFolder.EndsWith("\\"))
                    RuntimeInfo.ApplicationFolder += "\\";

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new Service1() };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception ex)
            {
                string msg = "#---------------------------------------------------------- \r\n"
                    + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\nScheduledTask.Service定时任务计划出错，详细错误信息为：" + ex;
                ServiceLog.SaveLog(msg);
            }
        }
    }
}
