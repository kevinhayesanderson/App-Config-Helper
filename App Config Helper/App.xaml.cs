using System.Windows;

namespace UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
