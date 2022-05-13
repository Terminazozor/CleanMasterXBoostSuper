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
    internal class Settings
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
        public void AddCopyTask()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.Description = "Ouvez le fichier que vous voulez copier\nAppuyer sur annuler pour stopper";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    List<string> fileToNotCopy = new List<string>();
                    SelectionOfFile selectionOfFile = new SelectionOfFile(dialog.SelectedPath, fileToNotCopy);
                    selectionOfFile.ShowDialog();
                    string[] splitPath = dialog.SelectedPath.Split('\\');
                    string folderName = splitPath[splitPath.Length - 1];
                    string stringFile = ListToString(fileToNotCopy);
                    CopyTask copyTaskToAdd = new CopyTask(dialog.SelectedPath, Destination + @"\" + day + @"\" + folderName, stringFile);
                    CopyTasks.Add(copyTaskToAdd);
                    AddCopyTask();
                }
                else
                {
                    throw new Exception("Aucun fichier selectionné");
                }
            }
        }
        public string ListToString(List<string> fileToNotCopy)
        {
            if (fileToNotCopy.Count == 0)
            {
                return null;
            }
            else
            {
                string temp = fileToNotCopy.First();
                fileToNotCopy.Remove(fileToNotCopy.First());
                foreach (string file in fileToNotCopy)
                {
                    temp = temp + "+" + file;
                }
                return temp;
            }
        }
        public Boolean ReadSettings()
        {
            try
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(@".\SettingsSave.resx"))
                {
                    Destination = resxSet.GetString("Destination");
                    Mail = resxSet.GetString("Mail");
                    string[] allSource = resxSet.GetString("Source").Split('|');
                    for(int i = 0; i < allSource.Length-1; i++)
                    {
                        string[] splitPath = allSource[i].Split('\\');
                        string folderName = splitPath[splitPath.Length - 1];
                        CopyTasks.Add(new CopyTask(allSource[i], Destination + @"\" + day + @"\" + folderName, null));
                    }
                    return true;
                }          
            }
            catch (Exception e)
            {
                SaveSettings();
                return false;
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
                }
            }
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
