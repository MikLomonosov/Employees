using System;
using System.Windows;
using System.Windows.Input;

namespace Employees.Commands
{
    internal class CloseApplicationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var choice = MessageBox.Show("Вы точно хотите закрыть приложение?", "Закрытие приложения", MessageBoxButton.YesNo);

            if(choice == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            
        }
    }
}
