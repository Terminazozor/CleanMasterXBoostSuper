using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMasterXBoostSuper
{
    public partial class Form1 : Form
    {
        volatile Boolean stop;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            stop = true;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            stop = false;
            Cleaning c = new Cleaning();
            Settings s = new Settings();
            if (!s.ReadSettings())
            {
                c.ConfigCleaning();
            }
            else
            {
                Console.WriteLine("sorti read");
            }
            List<CopyTask> listCT = s.CopyTasks;
            Thread clean = new Thread(c.DoCleaning);
            clean.Start();
            CopyManager cm = new CopyManager(listCT);
            Thread copy = new Thread(new ParameterizedThreadStart(cm.run));
            copy.Start(stop);
            SendMail sm = new SendMail();
            sm.NewMail(c.Happening+cm.Happening, s.Mail);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(@".\SettingsSave.resx");
        }
    }
}
