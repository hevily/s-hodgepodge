using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ScheduledTask.Entity;
using ScheduledTask.Interface;

namespace ScheduledTask.Demo
{
    public class Class1 : MarshalByRefObject, ITask
    {
        TaskConfiguration ITask.Init(string applicationFolder)
        {
            return new TaskConfiguration()
            {
                TaskName = "Demo",
                LoopType = TaskLoopType.SecondInterval,
                LoopInterval = 3000
            };
        }

        bool ITask.CheckTaskHaveRun(TaskConfiguration config, string theActionTime)
        {
            return true;
        }

        TaskExecuteResult ITask.ExecuteTask(string applicationFolder, string theActionTime)
        {
            var path = Path.Combine(applicationFolder, "_demo.txt");
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now);
                }
            }

            return new TaskExecuteResult()
            {
                IsSuccess = true
            };
        }


    }
}
