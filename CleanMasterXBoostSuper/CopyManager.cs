using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            long sumFile=0;
            foreach (CopyTask CopyTask in CopyTasks)
            {
                sumFile = sumFile+DirSize(new DirectoryInfo(CopyTask.Source));
            }
            string disk = CopyTasks.First().Destination.Split('\\')[0];
            long freeSpace = FreeSpaceDisk(disk);
            Console.WriteLine("freeSpace ="+freeSpace+" sumFile ="+sumFile);
            if(freeSpace > sumFile)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static long DirSize(DirectoryInfo d)
        {
            long Size = 0;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                Size += fi.Length;
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Size += DirSize(di);
            }
            return (Size);
        }
        public static long FreeSpaceDisk(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == (driveName+@"\"))
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }
        public void run()
        {
            foreach(CopyTask task in CopyTasks)
            {
                try
                {
                    Console.WriteLine(isLowFreeSpace());
                    if (!isLowFreeSpace())
                    {
                        ProcessStartInfo infos = new ProcessStartInfo("xcopy", task.Source + " " + task.Destination + " /e /h /v /d /y /i");
                        Console.WriteLine("fichier: " + task.Source + " copier dans " + task.Destination);
                        Process proc = Process.Start(infos);
                        AllTasksCompleted.Add(task);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public int TaskState()
        {
            long sumDid = 0;
            long sumToDo = 0;
            foreach (CopyTask CopyTask in CopyTasks)
            {
                sumToDo = sumToDo + DirSize(new DirectoryInfo(CopyTask.Source));
            }
            foreach (CopyTask CopyTask in CopyTasks)
            {
                sumDid = sumDid + DirSize(new DirectoryInfo(CopyTask.Source));
            }
            return 0;
        }
    }
}
