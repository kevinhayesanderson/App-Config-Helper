using System.Windows;
using System.Windows.Forms;

namespace UI.UserControls
{
    public partial class BrowseFolder : System.Windows.Controls.UserControl
    {
        public BrowseFolder() => InitializeComponent();

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(BrowseFolder), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Text 
        { 
            get => GetValue(TextProperty) as string;
            set => SetValue(TextProperty, value);
        }

        public static DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(BrowseFolder), new PropertyMetadata(null));
        public string Description 
        { 
            get => GetValue(DescriptionProperty) as string;
            set => SetValue(DescriptionProperty, value);
        }

        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = Description;
                dlg.SelectedPath = Text;
                dlg.ShowNewFolderButton = true;
                var result = dlg.ShowDialog();
                if (result != DialogResult.OK) return;
                Text = dlg.SelectedPath;
                var be = GetBindingExpression(TextProperty);
                be?.UpdateSource();
            }
        }
    }
}
