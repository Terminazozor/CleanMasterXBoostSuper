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
        public Settings()
        {
            CopyTasks = new List<CopyTask>();
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
                dialog.Description = "Ouvez le fichier que vous voulez copier";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    List<string> fileToNotCopy = new List<string>();
                    SelectionOfFile selectionOfFile = new SelectionOfFile(dialog.SelectedPath, fileToNotCopy);
                    selectionOfFile.ShowDialog();
                    String[] splitPath = dialog.SelectedPath.Split('\\');
                    String folderName = splitPath[splitPath.Length - 1];
                    String stringFile = ListToString(fileToNotCopy);
                    CopyTask copyTaskToAdd = new CopyTask(dialog.SelectedPath, Destination + @"\" + DateTime.Now.ToString("dddd") + @"\" + folderName, stringFile);
                    CopyTasks.Add(copyTaskToAdd);
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
        public void ReadSettings()
        {
            try
            {
                using (ResXResourceReader resxReader = new ResXResourceReader(@".\SettingsSave.resx"))
                {
                    foreach (DictionaryEntry entry in resxReader)
                    {
                        Console.WriteLine(entry.Value.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                SaveSettings();
            }
        }
        public void SaveSettings()
        {
            using (ResXResourceWriter resx = new ResXResourceWriter(@".\SettingsSave.resx"))
            {
                SetDestination();
                resx.AddResource("Destination",Destination);
            }
        }
    }
}
