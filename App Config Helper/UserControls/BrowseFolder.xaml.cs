using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;

namespace UI.UserControls
{
    public partial class BrowseFolder : System.Windows.Controls.UserControl
    {
        public BrowseFolder() => InitializeComponent();

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(BrowseFolder), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Text { get { return GetValue(TextProperty) as string; } set { SetValue(TextProperty, value); } }

        public static DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(BrowseFolder), new PropertyMetadata(null));
        public string Description { get { return GetValue(DescriptionProperty) as string; } set { SetValue(DescriptionProperty, value); } }

        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = Description;
                dlg.SelectedPath = Text;
                dlg.ShowNewFolderButton = true;
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Text = dlg.SelectedPath;
                    BindingExpression be = GetBindingExpression(TextProperty);
                    if (be != null)
                    {
                        be.UpdateSource();
                    }
                }
            }
        }
    }
}
