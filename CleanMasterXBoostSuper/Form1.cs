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
        CancellationTokenSource cts =new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            Cleaning c = new Cleaning();
            Settings s = new Settings();
            if (!s.ReadSettings())
            {
                c.ConfigCleaning();
            }
            List<CopyTask> listCT = s.CopyTasks;
            CancellationToken token = cts.Token;
            var taskClean = Task.Run(() => c.DoCleaning(token));
            CopyManagerRobocopy cm = new CopyManagerRobocopy(listCT);
            //CopyManager cm = new CopyManager(listCT);
            var taskCopy = Task.Run(() => cm.run(token));
            await Task.WhenAll(taskClean, taskCopy);
            //SendMail sm = new SendMail();
            //sm.NewMail(c.Happening+cm.Happening, s.Mail);
            //ProcessStartInfo startInfo =
            //new ProcessStartInfo("shutdown.exe", "-s /t 0");
            //Process.Start(startInfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(@".\SettingsSave.resx");
        }
    }
}
