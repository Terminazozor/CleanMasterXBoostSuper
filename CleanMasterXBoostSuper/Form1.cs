using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMasterXBoostSuper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cleaning c = new Cleaning();
            Settings s = new Settings();
            if (!s.ReadSettings())
            {
                c.ConfigCleaning();
            }
            List<CopyTask> listCT = s.CopyTasks;
            Thread clean = new Thread(c.DoCleaning);
            clean.Start();
            CopyManager cm = new CopyManager(listCT);
            Thread copy = new Thread(cm.run);
            copy.Start();
            clean.Join();
            copy.Join();
            //this.Close();
        }

    }
}
