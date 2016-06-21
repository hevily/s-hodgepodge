using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace ScheduledTask.Service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStop()
        {
            RuntimeInfo.ContinueWork = false;
            ServiceLog.SaveLog("服务结束！" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n");
        }

        protected override void OnStart(string[] args)
        {
            ServiceLog.SaveLog("服务启动！" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n");

            //初始化任务信息
            TaskPolling.InitTaskInfos();
            //启动轮询进程
            Thread wt = new Thread(new ThreadStart(TaskPolling.PollingThreadEntrance));
            wt.Start();
        }
    }
}
