using System.Windows;
using UI.ViewModel;

namespace UI
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) => _viewModel.Load();
    }
}
