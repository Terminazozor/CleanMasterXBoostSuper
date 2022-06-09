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
        int stop=0;
        List<CopyTask> AllTasksCompleted = new List<CopyTask>();
        public List<CopyTask> CopyTasks { get; private set; }
        public string Happening { get; private set; }
        public CopyManager(List<CopyTask> CopyTasks)
        {
            this.CopyTasks = CopyTasks;
            Happening = "";
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
            CopyProgress cp = new CopyProgress();
            if (!isLowFreeSpace())
            {
                while (CopyTasks.Count > 0)
                {
                    CopyTask task = CopyTasks.First();
                    File.WriteAllText(@"./ToNotCopy.txt", task.FileToNoCopy);
                    ProcessStartInfo infos = new ProcessStartInfo("xcopy", task.Source + " " + task.Destination + "/q /e /h /v /d /y /i /exclude:ToNotCopy.txt");
                    infos.CreateNoWindow = true;
                    infos.UseShellExecute = false;
                    Process proc = new Process();
                    proc = Process.Start(infos);
                    AllTasksCompleted.Add(task);
                    Console.WriteLine(task.Destination);
                    proc.WaitForExit();
                    if (proc.ExitCode != 0)
                    {
                        Happening = Happening + "Copie de " + task.Source + " dans " + task.Destination + " echec\n";
                    }
                    else
                    {
                        Happening = Happening + "Copie de " + task.Source + " dans " + task.Destination + " reussi\n";
                    }
                    CopyTasks.Remove(task);
                }
            }
            File.Delete(@"./ToNotCopy.txt");
        }
        public int TaskState()
        {
            long sumDid = 0;
            long sumToDo = 0;
            foreach (CopyTask CopyTask in CopyTasks)
            {
                sumToDo = sumToDo + DirSize(new DirectoryInfo(CopyTask.Source));
            }
            foreach (CopyTask CopyTask in AllTasksCompleted)
            {
                sumDid = sumDid + DirSize(new DirectoryInfo(CopyTask.Source));
            }
            float div=(float)sumDid /sumToDo;
            float pourcent = div * 100;
            return (int)pourcent;
        }
    }
}
