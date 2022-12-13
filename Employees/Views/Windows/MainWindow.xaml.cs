using System.Windows;


namespace Employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }  
        
        private void DraggableWindow(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
    }
}
