using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMasterXBoostSuper
{
    internal partial class Settings
    {
        public string Destination { get; private set; }
        public List<CopyTask> CopyTasks { get; private set; }
        public string Mail { get; private set; }
        string day; 
        public Settings()
        {
            CopyTasks = new List<CopyTask>();
            day = DateTime.Now.ToString("dddd");
            Mail = "";
        }
        public void SetDestination()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.Description = "Ouvez le fichier où vous voulez stocker vos copies";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    Destination = dialog.SelectedPath;
                }
                else
                {
                    throw new Exception("Aucun fichier selectionné");
                }

            }

        }
        public string ListToString(List<string> fileToNotCopy)
        {
            if (fileToNotCopy.First()=="")
            {
                return "";
            }
            else
            {
                string temp = "";
                foreach (string file in fileToNotCopy)
                {
                    temp = temp+file+"\n";
                }
                return temp;
            }
        }
        public void SaveSettings()
        {
            using (ResXResourceWriter resx = new ResXResourceWriter(@".\SettingsSave.resx"))
            {
                while (String.IsNullOrEmpty(Mail)) 
                {
                    Mail = Interaction.InputBox("Entrez votre adresse mail :", "Mail", "");
                }
                SetDestination();
                resx.AddResource("Destination",Destination);
                resx.AddResource("Mail", Mail);
                try
                {
                    AddCopyTask();
                }
                catch(Exception e)
                {
                    resx.AddResource("Source", SaveTask());
                    resx.AddResource("FileToNotCopy", SaveFileNotToCopy());
                }
                
            }
        }
        public string SaveFileNotToCopy()
        {
            string temp = "";
            foreach (CopyTask task in CopyTasks)
            {
                temp = temp + task.FileToNoCopy + "|";
            }
            return temp;
        }
        public string SaveTask()
        {
            string temp="";
            foreach (CopyTask task in CopyTasks)
            {
                temp = temp + task.Source+"|";
            }
            return temp;
        }
    }
}
