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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cleaning c = new Cleaning();
            Settings s = new Settings();
            if (!s.ReadSettings())
            {
                c.ConfigCleaning();
            }
            List<CopyTask> listCT = s.CopyTasks;
            CopyManager cm = new CopyManager(listCT);
            cm.run();
        }
    }
}
