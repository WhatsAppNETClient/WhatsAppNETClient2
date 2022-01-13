using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoWhatsAppNETAPICSharp
{
    public class Helper
    {
        public static string ShowDialogOpen(string title, bool fileImageOnly = false)
        {
            var fileName = string.Empty;

            using (var dlgOpen = new OpenFileDialog())
            {
                dlgOpen.Filter = fileImageOnly ? "File gambar (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png"
                                               : "File dokumen (*.*)|*.*";
                dlgOpen.Title = title;

                var result = dlgOpen.ShowDialog();
                if (result == DialogResult.OK) fileName = dlgOpen.FileName;
            }

            return fileName;
        }

        public static string ShowDialogOpenFolder()
        {
            var folderName = string.Empty;

            using (var dlgOpen = new FolderBrowserDialog())
            {
                var result = dlgOpen.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dlgOpen.SelectedPath))
                {
                    folderName = dlgOpen.SelectedPath;
                }
            }

            return folderName;
        }
    }
}
