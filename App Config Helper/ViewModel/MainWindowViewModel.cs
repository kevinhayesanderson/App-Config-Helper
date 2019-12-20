namespace UI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IHeaderViewModel _headerViewModel;

        public MainWindowViewModel(IHeaderViewModel headerViewModel)
        {
            _headerViewModel = headerViewModel;
        }

        public void Load() => _headerViewModel.Load();

        public IHeaderViewModel HeaderViewModel
        {
            get => _headerViewModel;
            set
            {
                _headerViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
