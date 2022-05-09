using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class Saver
    {
        public static void Save(string SaveFolder, string FileName, string SaveData, string extension = ".txt")
        {
            try
            {
                string FullPathFile = $"{SaveFolder}\\{FileName}{extension}";
                using (FileStream fileStream = new FileStream(FullPathFile, FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.Write(SaveData);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
