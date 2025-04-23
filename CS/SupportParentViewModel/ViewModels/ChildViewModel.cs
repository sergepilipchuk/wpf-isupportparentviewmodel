using DevExpress.Mvvm;
using System.Windows.Input;

namespace SupportParentViewModel.ViewModels {
    internal class ChildViewModel : ViewModelBase {
        public ICommand ShowMessageCommand { get; private set; }

        public ChildViewModel() {
            ShowMessageCommand = new DelegateCommand(ShowMessage);
        }

        IMessageBoxService MessageBoxService {
            get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); }
        }

        void ShowMessage() {
            MessageBoxService.Show(
                "Child view correctly utilizes parent's DXMessageBoxService");
        }

        //The property doesn't raise notifications out of the box
        protected override void OnParentViewModelChanged(object parentViewModel) {
            RaisePropertyChanged(nameof(ISupportParentViewModel.ParentViewModel));
        }
    }
}