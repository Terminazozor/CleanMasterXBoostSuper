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
            Cleaning c= new Cleaning();
            c.ConfigCleaning();
            c.LunchCleaning();
            
            //afficher un fichier dans une listbox
            /*System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("c:\\");
            System.IO.FileSystemInfo[] files = di.GetFileSystemInfos();
            checkedListBox1.Items.AddRange(files);*/
        }
    }
}
