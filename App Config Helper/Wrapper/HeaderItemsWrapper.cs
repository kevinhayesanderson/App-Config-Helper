using Models;
using System.Runtime.CompilerServices;
using UI.ViewModel;

namespace UI.Wrapper
{
    public class HeaderItemsWrapper : ViewModelBase
    {
        private bool _isChanged;

        public HeaderItemsWrapper(HeaderItems headerItems) => Model = headerItems;

        public HeaderItems Model { get; }

        public bool IsChanged
        {
            get { return _isChanged; }
            private set
            {
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        public string Path
        {
            get { return Model.Path; }
            set
            {
                Model.Path = value;
                OnPropertyChanged();
            }
        }

        public string ConfigType
        {
            get { return Model.ConfigType; }
            set
            {
                Model.ConfigType = value;
                OnPropertyChanged();
            }
        }

        public string FilterProperty
        {
            get { return Model.FilterProperty; }
            set
            {
                Model.FilterProperty = value;
                OnPropertyChanged();
            }
        }

        public void AcceptChanges() => IsChanged = false;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
        }
    }
}
