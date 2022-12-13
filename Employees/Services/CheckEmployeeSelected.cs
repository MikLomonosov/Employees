using Employees.ViewModels.Base;
using System;

namespace Employees.Services
{
    public class CheckEmployeeSelected : EventArgs
    {
        public bool IsSelected;

        public BaseViewModel ViewModel;

        public CheckEmployeeSelected(bool isSelected, BaseViewModel viewModel)
        {
            this.IsSelected = isSelected;
            this.ViewModel = viewModel;
        }
    }
}
