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
    public partial class CopyProgress : Form
    {
        int Progress;
        public CopyProgress()
        {
            InitializeComponent();
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            Progress = 0;
        }
        public void UpdateProgress(int progress,string file)
        {
            progressBar1.Value= progress;
            label1.Text= file;
        }
    }
}
