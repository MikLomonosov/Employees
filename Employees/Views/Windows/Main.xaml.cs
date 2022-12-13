using System.Windows;

namespace Employees
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }
        private void DraggableWindow(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonShowMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonShowMenu.Visibility = Visibility.Collapsed;
            ButtonHideMenu.Visibility = Visibility.Visible;
        }

        private void ButtonHideMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonShowMenu.Visibility = Visibility.Visible;
            ButtonHideMenu.Visibility = Visibility.Collapsed;
        }
    }
}
