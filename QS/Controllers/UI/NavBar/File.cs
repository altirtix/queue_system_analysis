using Microsoft.Win32;
using System;
using System.Windows;

namespace QS.Controllers.UI.NavBar
{
    public static class File
    {
        public static bool OpenFile()
        {
            try
            {
                string FilePath;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    FilePath = openFileDialog.FileName;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message");
            }
            return false;
        }

        public static bool SaveFile()
        {
            try
            {
                string FilePath;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    FilePath = saveFileDialog.FileName;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message");
            }
            return false;
        }
    }
}
