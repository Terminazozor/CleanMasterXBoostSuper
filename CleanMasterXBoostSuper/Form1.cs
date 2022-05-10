using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMasterXBoostSuper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CopyTask ct = new CopyTask(@"C:\Users\Valentin\Documents\Cours\bloc2\c#\test\a_copier", @"C:\Users\Valentin\Documents\Cours\bloc2\c#\test\copier");
            CopyTask ct2 = new CopyTask(@"C:\Users\Valentin\Documents\Cours\bloc2\c#\test\copier2", @"C:\Users\Valentin\Documents\Cours\bloc2\c#\test\copier");
            List<CopyTask> list = new List<CopyTask>();
            list.Add(ct);
            list.Add(ct2);
            CopyManager cm = new CopyManager(list);
            cm.run();
        }
    }
}
