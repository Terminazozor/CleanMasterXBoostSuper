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
                ProcessStartInfo infos = new ProcessStartInfo("xcopy",task.Source+" "+task.Destination+" /e");
                Process proc = Process.Start(infos);
                AllTasksCompleted.Add(task);
            }
        }
    }
}
