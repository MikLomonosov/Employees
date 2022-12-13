using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services.DialogService
{
    public interface IDialogService
    {
        void ShowMessage(string message, string caption);
        string FilePath { get; set; }
        bool OpenFileDialog();
    }
}
