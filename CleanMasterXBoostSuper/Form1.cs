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
            //afficher un fichier dans une listbox
            /*System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("c:\\");
            System.IO.FileSystemInfo[] files = di.GetFileSystemInfos();
            checkedListBox1.Items.AddRange(files);*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            s.ReadSettings();
            /*s.SetDestination();
            s.AddCopyTask();
            List<CopyTask> listCT = s.CopyTasks;
            CopyManager cm = new CopyManager(listCT);
            cm.run();*/
        }
    }
}
