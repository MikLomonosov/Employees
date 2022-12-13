using Employees.ViewModels.Base;
using System;

namespace Employees.Services.ChangeContent
{
    public class ChangeContent : IChangeContent
    {
        #region fields
        public event EventHandler<CheckEmployeeSelected> OnSelectedEmployee;
        #endregion

        #region methods
        public void ChangeViewModel(BaseViewModel viewModel)
        {
            OnSelectedEmployee?.Invoke(this, new CheckEmployeeSelected(true, viewModel));
        }
        #endregion
    }
}
