using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMasterXBoostSuper
{
    internal class CopyTask
    {
        public string Destination { get;}
        public string Source { get;}
        public List<string> FileToNoCopy { get; }
        public CopyTask(string source,string destination, List<string> fileToNoCopy)
        {
            this.Destination = destination;
            this.Source = source;
            this.FileToNoCopy = fileToNoCopy;
        }
    }
}
