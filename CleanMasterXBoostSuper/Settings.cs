using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMasterXBoostSuper
{
    internal class Settings
    {
        string Destination;
        List<CopyTask> CopyTasks;
        public void SetDestination()
        {
            using(FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.Description = "Ouvez le fichier où vous voulez stocker vos copies";
                DialogResult result = dialog.ShowDialog();
                if (result==DialogResult.OK && !string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    Destination = dialog.SelectedPath;
                }
                else
                {
                    throw new Exception("Aucun fichier selectionné");
                }

            }
                
        }
        public void SetCopyTask()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.Description = "Ouvez le fichier que vous voulez copier";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    
                }
                else
                {
                    throw new Exception("Aucun fichier selectionné");
                }
            }
        }
    }

}
