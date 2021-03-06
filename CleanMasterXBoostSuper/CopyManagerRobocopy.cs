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
    internal class CopyManagerRobocopy
    {
        List<CopyTask> AllTasksCompleted = new List<CopyTask>();
        List<CopyTask> CopyTasks;
        public string Happening { get; private set; }
        public CopyManagerRobocopy(List<CopyTask> CopyTasks)
        {
            this.CopyTasks = CopyTasks;
            Happening = "";
        }
        public Boolean isLowFreeSpace()
        {
            long sumFile = 0;
            foreach (CopyTask CopyTask in CopyTasks)
            {
                sumFile = sumFile + DirSize(new DirectoryInfo(CopyTask.Source));
            }
            string disk = CopyTasks.First().Destination.Split('\\')[0];
            long freeSpace = FreeSpaceDisk(disk);
            if (freeSpace > sumFile)
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
                if (drive.IsReady && drive.Name == (driveName + @"\"))
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }
        public void run(CancellationToken token)
        {
            CopyProgress cp = new CopyProgress();
            cp.Show();
            cp.UpdateProgress(0, "Start");
            if (!token.IsCancellationRequested)
            {
                cp.Close();
            }
            foreach (CopyTask task in CopyTasks)
            {
                if (!token.IsCancellationRequested)
                {
                    cp.Update();
                    if (!isLowFreeSpace())
                    {
                        ProcessStartInfo infos = new ProcessStartInfo("robocopy.exe", task.Source + " " + task.Destination + " /mir /XO ");
                        infos.CreateNoWindow = true;
                        infos.UseShellExecute = false;
                        Process proc = new Process();
                        proc = Process.Start(infos);
                        AllTasksCompleted.Add(task);
                        Console.WriteLine(task.Destination);
                        cp.UpdateProgress(TaskState(), "Copie de " + task.Source);
                        proc.WaitForExit();
                        if (proc.ExitCode != 0)
                        {
                            Happening = Happening + "Copie de " + task.Source + " dans " + task.Destination + " echec\n";
                        }
                        else
                        {
                            Happening = Happening + "Copie de " + task.Source + " dans " + task.Destination + " reussi\n";
                        }
                    }
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
            foreach (CopyTask CopyTask in AllTasksCompleted)
            {
                sumDid = sumDid + DirSize(new DirectoryInfo(CopyTask.Source));
            }
            float div = (float)sumDid / sumToDo;
            float pourcent = div * 100;
            return (int)pourcent;
        }
    }
}
