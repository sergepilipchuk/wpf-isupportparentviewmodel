Imports System.Windows.Input
Imports DevExpress.Mvvm

Namespace SupportParentViewModel.ViewModels

    Public Class MainViewModel
        Inherits ViewModelBase

        Private _ShowMessageCommand As ICommand

        Public Property ParentProperty As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property ShowMessageCommand As ICommand
            Get
                Return _ShowMessageCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ShowMessageCommand = value
            End Set
        End Property

        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return GetService(Of IMessageBoxService)()
            End Get
        End Property

        Public Sub New()
            ShowMessageCommand = New DelegateCommand(AddressOf ShowMessage)
            ParentProperty = "Shared property"
        End Sub

        Private Sub ShowMessage()
            MessageBoxService.Show("MainView utilizes its own DXMessageBoxService")
        End Sub
    End Class
End Namespace
