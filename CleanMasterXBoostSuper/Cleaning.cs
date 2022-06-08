using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        public void DoCleaning(CancellationToken token) {
            if (LunchCleaning(token) == 0)
            {
                Happening = "Nettoyage reussis\n";
            }
            else
            {
                Happening = "Erreur dans le nettoyage\n";
            }
        }
        public int LunchCleaning(CancellationToken token)
        {
            ProcessStartInfo infos = new ProcessStartInfo("cleanmgr.exe","sagerun:1");
            using(Process proc = Process.Start(infos))
            {
                while (!proc.HasExited)
                {
                    if (token.IsCancellationRequested)
                    {
                        proc.Kill();
                    }
                }
                return proc.ExitCode;
            }
        }
    }
}
