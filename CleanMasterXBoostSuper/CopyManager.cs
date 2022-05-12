using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMasterXBoostSuper
{
    internal class CopyManager
    {
        List<CopyTask> AllTasksCompleted = new List<CopyTask>();
        List<CopyTask> CopyTasks;
        public CopyManager(List<CopyTask> CopyTasks)
        {
            this.CopyTasks = CopyTasks;
        }
        public Boolean isLowFreeSpace()
        {
            return false;
        }
        public void run()
        {
            foreach(CopyTask task in CopyTasks)
            {
                try
                {
                    ProcessStartInfo infos = new ProcessStartInfo("xcopy", task.Source + " " + task.Destination + " /e /h /v /d /y /i");
                    Console.WriteLine("fichier: " + task.Source + " copier dans " + task.Destination);
                    Process proc = Process.Start(infos);
                    AllTasksCompleted.Add(task);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
