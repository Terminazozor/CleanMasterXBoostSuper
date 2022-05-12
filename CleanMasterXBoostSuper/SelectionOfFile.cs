using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMasterXBoostSuper
{
    public partial class SelectionOfFile : Form
    {
        List<string> FileToNotCopy;
        public SelectionOfFile(string filePath,List<string>fileToNotCopy)
        {
            InitializeComponent();
            this.FileToNotCopy = fileToNotCopy;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(filePath);
            System.IO.FileSystemInfo[] files = di.GetFileSystemInfos();
            string[] namefile = new string[files.Length];
            for(int i = 0; i < files.Length; i++)
            {
                namefile[i] = files[i].Name;
            }
            checkedListFile.Items.AddRange(namefile);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(object checkeditem in checkedListFile.CheckedItems)
            {
                FileToNotCopy.Add((string)checkeditem.ToString());
            }
            this.Close();
        }
    }
}
