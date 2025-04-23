Imports DevExpress.Mvvm
Imports System.Windows.Input

Namespace SupportParentViewModel.ViewModels

    Friend Class ChildViewModel
        Inherits ViewModelBase

        Private _ShowMessageCommand As ICommand

        Public Property ShowMessageCommand As ICommand
            Get
                Return _ShowMessageCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ShowMessageCommand = value
            End Set
        End Property

        Public Sub New()
            ShowMessageCommand = New DelegateCommand(AddressOf ShowMessage)
        End Sub

        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return GetService(Of IMessageBoxService)(ServiceSearchMode.PreferParents)
            End Get
        End Property

        Private Sub ShowMessage()
            MessageBoxService.Show("Child view correctly utilizes parent's DXMessageBoxService")
        End Sub

        'The property doesn't raise notifications out of the box
        Protected Overrides Sub OnParentViewModelChanged(ByVal parentViewModel As Object)
            RaisePropertyChanged(NameOf(ISupportParentViewModel.ParentViewModel))
        End Sub
    End Class
End Namespace
