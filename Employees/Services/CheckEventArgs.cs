using System;

namespace Employees.Services
{
    public class CheckEventArgs : EventArgs
    {
        public bool IsChecked;

        public CheckEventArgs(bool isChecked)
        {
            this.IsChecked = isChecked;
        }
    }
}
