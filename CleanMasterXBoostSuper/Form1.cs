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
using System.Windows.Forms;

namespace CleanMasterXBoostSuper
{
    public partial class Form1 : Form
    {
        Thread clean;
        Thread copy;
        Boolean stop = false;
        int nbStop = 0;
        volatile List<CopyTask> listCT;
        CopyManager cm;
        Cleaning c;
        Settings s;
        volatile Boolean pause = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clean.Abort();
            copy.Abort();
            stop = true;
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            c = new Cleaning();
            s = new Settings();
            if (!s.ReadSettings())
            {
                c.ConfigCleaning();
            }
            listCT = s.CopyTasks;
            //CopyManagerRobocopy cm = new CopyManagerRobocopy(listCT);
            cm = new CopyManager(listCT);
            clean = new Thread(c.DoCleaning);
            clean.Start();
            copy = new Thread(cm.run);
            copy.Start();
            Thread close = new Thread(new ParameterizedThreadStart(closing));
            object[] param ={ this, listCT };
            close.Start(param);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(@".\SettingsSave.resx");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Pause")
            {
                nbStop++;
                listCT = cm.CopyTasks;
                copy.Abort();
                button3.Text = "Repprendre";
            }
            else
            {
                if (listCT.Count>0)
                {
                    cm = new CopyManager(listCT);
                    copy = new Thread(cm.run);
                    copy.Start();
                }
                button3.Text = "Pause";
            }
        }
        public void closing(object o)
        {
            object[] temp = (object[])o;
            Form f1 = (Form)temp[0];
            List < CopyTask > tasks = (List < CopyTask>)temp[1];
            while (tasks.Count>0)
            {

            }
            if (!stop)
            {
                //SendMail sm = new SendMail();
                //sm.NewMail(c.Happening+cm.Happening, s.Mail);
                //ProcessStartInfo startInfo =
                //new ProcessStartInfo("shutdown.exe", "-s /t 0");
                //Process.Start(startInfo);
            }
            else
            {
                f1.Close();
            }

        }

    }
}
