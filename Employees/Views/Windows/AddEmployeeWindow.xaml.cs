using System.Windows;


namespace Employees.Views.Windows
{
    /// <summary>
    /// Interaction logic for ManageEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }
        private void DraggableWindow(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            var dialogResult =  MessageBox.Show(messageBoxText: "Вы действительно хотите закрыть окно регистрации?", "Уведомление", MessageBoxButton.OKCancel);
            if(dialogResult == MessageBoxResult.OK)
            {
                this.Close();
            }
        }
    }
}
