using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace SupportParentViewModel.ViewModels {
    public class CustomChildViewModel : ISupportParentViewModel, ISupportServices, INotifyPropertyChanged {
        public ICommand ShowMessageCommand { get; private set; }

        public CustomChildViewModel() {
            ShowMessageCommand = new DelegateCommand(ShowMessage);
        }

        IMessageBoxService MessageBoxService {
            get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); }
        }

        void ShowMessage() {
            MessageBoxService.Show("Child view correctly utilizes parent's DXMessageBoxService");
        }

        #region ISupportServices

        public object ParentViewModel {
            get { return _parentViewModel; }
            set {
                if (Equals(value, _parentViewModel)) return;
                _parentViewModel = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ISupportServices

        protected virtual T GetService<T>(ServiceSearchMode searchMode) where T : class {
            return this.ServiceContainer.GetService<T>(searchMode);
        }

        IServiceContainer serviceContainer;
        private object _parentViewModel;

        protected IServiceContainer ServiceContainer {
            get {
                if (serviceContainer == null)
                    serviceContainer = new ServiceContainer(this);
                return serviceContainer;
            }
        }

        IServiceContainer ISupportServices.ServiceContainer {
            get { return ServiceContainer; }
        }

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}