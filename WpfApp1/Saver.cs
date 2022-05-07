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
        public static void Save(string SaveFolder, string FileName, string SaveData, string extension = "txt")
        {
            try
            {
                FileName += $".{extension}";
                string FullPathNewFile = $"{SaveFolder}\\{FileName}";
                using (FileStream fs = new FileStream(FullPathNewFile, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(SaveData);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка :\n{e.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
