using Employees.Services;
using Employees.Services.ChangeContent;
using Employees.ViewModels;
using Employees.ViewModels.Base;
using Employees.Views;
using System;
using System.IO;
using System.Windows;

namespace Employees
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region fields
        private EmployeeViewModel employeeViewModel;
        private ChangeContent changeContent;
        private MainWindow mainWindow;
        private Main employeeWindow;
        private MainWindowViewModel mainWindowViewModel;
        private string projectPath = Directory.GetCurrentDirectory();
        private BaseViewModel closeViewModel;
        #endregion

        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (FileStream fileStream = new FileStream($"{projectPath}/UsingStyle.txt", FileMode.OpenOrCreate))
            {
                byte[] usingStyleNameBytes = new byte[fileStream.Length];
                string usingStyleName = null;

                await fileStream.ReadAsync(usingStyleNameBytes, 0, usingStyleNameBytes.Length);

                if(usingStyleNameBytes.Length < 1)
                {
                    usingStyleName = "Light";
                }
                else
                {
                    usingStyleName = System.Text.Encoding.Default.GetString(usingStyleNameBytes);
                }
                var uri = new Uri("/Services/Styles/" + usingStyleName + ".xaml", UriKind.Relative);
                //get resource dictionary 
                ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;

                //clear application resource collection
                Application.Current.Resources.Clear();

                //add got resource dictionary
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.OnAuthorize += LoginViewModelOnOnAuthorize;
            mainWindow = new MainWindow() 
            { 
                DataContext = mainWindowViewModel 
                
            };
            mainWindow.Show();



            changeContent = new ChangeContent();

        }

        #region methods
        /// <summary>
        /// method checks user authorized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginViewModelOnOnAuthorize(object sender, LoginEventArgs e)
        { 
            if (e.IsAuthorized)
            {
                employeeViewModel = new EmployeeViewModel(e.User, changeContent, e.IsAuthorized);
                employeeWindow = new Main()
                {
                    DataContext = employeeViewModel
                };
                employeeViewModel.OnLogOut += LogingOut;
                employeeViewModel.OnSwitchTheme += ChangeTheme;
                employeeWindow.Show();
                mainWindow.Close();

                return;
            }
        }

        private void LogingOut(object sender, CheckEventArgs e)
        {
            if (e.IsChecked)
            {
                mainWindow = new MainWindow() { DataContext = mainWindowViewModel };
                mainWindow.Show();
                employeeWindow.Close();
                return;
            }

        }

        private async void ChangeTheme(object sender, ChangeThemeEventArgs e)
        {
            if(e.IsSelected)
            {
                //set file path
                var uri = new Uri("/Services/Styles/" + e.SelectedTheme + ".xaml", UriKind.Relative);

                //get resource dictionary 
                ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;

                //clear application resource collection
                Application.Current.Resources.Clear();

                //add got resource dictionary
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

                using(FileStream fileStream = new FileStream($"{projectPath}/UsingStyle.txt", FileMode.Truncate))
                {
                    byte[] usingStyleNameBytes = System.Text.Encoding.Default.GetBytes(e.SelectedTheme);

                    await fileStream.WriteAsync(usingStyleNameBytes, 0, usingStyleNameBytes.Length);
                }
            }
        }
        #endregion
    }
}
