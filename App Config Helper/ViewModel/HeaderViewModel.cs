using UI.Wrapper;

namespace UI.ViewModel
{
    public class HeaderViewModel : ViewModelBase, IHeaderViewModel
    {
        private HeaderItemsWrapper _headerItems;

        private HeaderItemsWrapper HeaderItems
        {
            get => _headerItems;
            set
            {
                _headerItems = value;
                OnPropertyChanged();
            }
        }

        public void Load()
        {
            HeaderItems = new HeaderItemsWrapper(new Models.HeaderItems());
            HeaderItems.PropertyChanged += Header_PropertyChanged;
        }

        private void Header_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }
    }
}
