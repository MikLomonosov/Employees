using Employees.ViewModels;
using Employees.Views.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace Employees.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private AddEmployeeWindow window;
        //private readonly Action<object> execute;
        //private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => this.window == null;

        public void Execute(object parameter)
        {
            var window = new AddEmployeeWindow
            {
                DataContext = new AddEmployeeWindowViewModel()
                //Owner = Application.Current.MainWindow
            };

            this.window = window;
            window.Closed += OnWindowClosed;

            window.ShowDialog();
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= OnWindowClosed;
            this.window = null;
        }
    }
}
