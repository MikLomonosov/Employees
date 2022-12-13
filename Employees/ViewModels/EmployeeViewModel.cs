using Employees.Services;
using Employees.Services.ChangeContent;
using Employees.Services.DialogService;
using Employees.ViewModels.Base;
using System;

namespace Employees.ViewModels
{
    internal class EmployeeViewModel : BaseViewModel
    {
        #region fields
        private EmployeeListViewModel employeeListViewModel;
        private BaseViewModel baseViewModel;
        private IChangeContent changeContent;
        public event EventHandler<CheckEventArgs> OnLogOut;
        public event EventHandler<ChangeThemeEventArgs> OnSwitchTheme;
        private DefaultDialogService dialogService;
        private ManageEmployeeViewModel manageEmployeeViewModel;
        #endregion

        #region properties

        private BaseViewModel currentContent;
        public BaseViewModel CurrentContent
        {
            get => currentContent;
            set
            {
                currentContent = value;
                OnPropertyChanged(nameof(CurrentContent));
            }
        }

        #endregion

        #region commands
        private readonly RelayCommand jumpToManageEmployeeView;
        public RelayCommand JumpToManageEmployeeView => jumpToManageEmployeeView;

        private readonly RelayCommand jumpToMainView;
        public RelayCommand JumpToMainView => jumpToMainView;

        private readonly RelayCommand logOut;
        public RelayCommand LogOut => logOut;
        #endregion

        #region constructor

        public EmployeeViewModel(object user, ChangeContent changeContent, bool isAuthorized)
        {
            manageEmployeeViewModel = new ManageEmployeeViewModel(user);
            manageEmployeeViewModel.OnSelectedTheme += checkSelectedTheme;

            jumpToManageEmployeeView = new RelayCommand(p => changeViewModel(manageEmployeeViewModel));

            this.changeContent = changeContent;
            currentContent = new EmployeeListViewModel(changeContent);


            jumpToMainView = new RelayCommand(p => changeViewModel(new EmployeeListViewModel(changeContent)));

            employeeListViewModel = new EmployeeListViewModel(changeContent);

            changeContent.OnSelectedEmployee += OnJumpToEmployeeDetailedInformation;

            logOut = new RelayCommand(p => executeLogOut(), p => true);

        }
        #endregion

        #region methods 

        private void checkSelectedTheme(object sender, ChangeThemeEventArgs e)
        {
            if(e.IsSelected)
            {
                OnSwitchTheme?.Invoke(this, new ChangeThemeEventArgs(true, e.SelectedTheme));
            }
        }


        private void executeLogOut()
        {
            dialogService = new DefaultDialogService();

            bool exitChoice = dialogService.ChoosePopup("Вы точно хотите выйти из аккаунта?", "Выход из учетной записи");

            if (exitChoice)
            {
                OnLogOut?.Invoke(this, new CheckEventArgs(true));
            }
        }
        private void OnJumpToEmployeeDetailedInformation(object sender, CheckEmployeeSelected e)
        {
            if (e.IsSelected)
            {
                CurrentContent = e.ViewModel;
            }
        }
        private void changeViewModel(BaseViewModel viewModel)
        {
            CurrentContent = viewModel;

            
        }
        #endregion

    }
}
