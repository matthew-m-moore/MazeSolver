using System.Windows;

namespace MazeSolver.WinApp
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}
