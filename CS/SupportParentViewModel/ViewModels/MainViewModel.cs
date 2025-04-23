using System.Windows.Input;
using DevExpress.Mvvm;

namespace SupportParentViewModel.ViewModels {
    public class MainViewModel : ViewModelBase {
        public string ParentProperty {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public ICommand ShowMessageCommand { get; private set; }

        IMessageBoxService MessageBoxService {
            get { return GetService<IMessageBoxService>(); }
        }

        public MainViewModel() {
            ShowMessageCommand = new DelegateCommand(ShowMessage);

            ParentProperty = "Shared property";
        }

        void ShowMessage() {
            MessageBoxService.Show("MainView utilizes its own DXMessageBoxService");
        }
    }
}