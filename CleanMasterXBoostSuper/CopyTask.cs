using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMasterXBoostSuper
{
    internal class CopyTask
    {
        public String Destination { get;}
        public String Source { get;}
        public CopyTask(String source,String destination)
        {
            this.Destination = destination;
            this.Source = source;
        }
    }
}
