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
        public void ConfigCleaning()
        {
            ProcessStartInfo infos = new ProcessStartInfo("cleanmgr.exe","sageset:1");
            Process proc = Process.Start(infos);
        }
        public void LunchCleaning()
        {
            ProcessStartInfo infos = new ProcessStartInfo("cleanmgr.exe","sagerun:1");
            Process proc = Process.Start(infos);
        }
    }
}
