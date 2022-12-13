using Employees.Services.DialogService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Services
{
    public class ChangeThemeEventArgs : EventArgs
    {
        public bool IsSelected;
        public string SelectedTheme;
        public ChangeThemeEventArgs(bool isSelected, string selectedTheme)
        {
            this.IsSelected = isSelected;
            this.SelectedTheme = selectedTheme;
        }
    }
}
