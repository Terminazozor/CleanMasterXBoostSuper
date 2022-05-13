using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMasterXBoostSuper
{
    public class Cleaning
    {
        public string Happening { get; private set; }
        public int ConfigCleaning()
        {
            ProcessStartInfo infos = new ProcessStartInfo("cleanmgr.exe","sageset:1");
            using (Process proc = Process.Start(infos))
            {
                if (!proc.HasExited)
                {
                    proc.WaitForExit();
                }
                return proc.ExitCode;
            }
        }
        public void DoCleaning() {
            if (LunchCleaning() == 0)
            {
                Happening = "Nettoyage reussis\n";
            }
            else
            {
                Happening = "Erreur dans le nettoyage\n";
            }
        }
        public int LunchCleaning()
        {
            ProcessStartInfo infos = new ProcessStartInfo("cleanmgr.exe","sagerun:1");
            using(Process proc = Process.Start(infos))
            {
                if (!proc.HasExited)
                {
                    proc.WaitForExit();
                }
                return proc.ExitCode;
            }
        }
    }
}
