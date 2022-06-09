using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMasterXBoostSuper
{
    internal partial class Settings
    {
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
        public Boolean ReadSettings()
        {
            try
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(@".\SettingsSave.resx"))
                {
                    Destination = resxSet.GetString("Destination");
                    Mail = resxSet.GetString("Mail");
                    string[] allSource = resxSet.GetString("Source").Split('|');
                    string[] allFileNotToCopy = resxSet.GetString("FileToNotCopy").Split('|');
                    for (int i = 0; i < allSource.Length - 1; i++)
                    {
                        string[] splitPath = allSource[i].Split('\\');
                        string folderName = splitPath[splitPath.Length - 1];
                        CopyTasks.Add(new CopyTask(allSource[i], Destination + @"\" + day + @"\" + folderName, allFileNotToCopy[i]));
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
    }
}
