using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Employees.Services.DialogService
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        public void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }
       
        public bool ChoosePopup(string message, string caption)
        {
            var chooseResult = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

            if (chooseResult == MessageBoxResult.Yes)
                return true;
            else
                return false;
        }
    }
}
