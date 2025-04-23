Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Windows.Input
Imports DevExpress.Mvvm

Namespace SupportParentViewModel.ViewModels

    Public Class CustomChildViewModel
        Implements ISupportParentViewModel, ISupportServices, INotifyPropertyChanged

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

#Region "ISupportServices"
        Public Property ParentViewModel As Object Implements ISupportParentViewModel.ParentViewModel
            Get
                Return _parentViewModel
            End Get

            Set(ByVal value As Object)
                If Equals(value, _parentViewModel) Then Return
                _parentViewModel = value
                OnPropertyChanged()
            End Set
        End Property

#End Region
#Region "ISupportServices"
        Protected Overridable Function GetService(Of T As Class)(ByVal searchMode As ServiceSearchMode) As T
            Return ServiceContainerProp.GetService(Of T)(searchMode)
        End Function

        Private serviceContainerField As IServiceContainer

        Private _parentViewModel As Object

        Protected ReadOnly Property ServiceContainerProp As IServiceContainer
            Get
                If serviceContainerField Is Nothing Then serviceContainerField = New ServiceContainer(Me)
                Return serviceContainerField
            End Get
        End Property

        Private ReadOnly Property ServiceContainer As IServiceContainer Implements ISupportServices.ServiceContainer
            Get
                Return ServiceContainerProp
            End Get
        End Property

#End Region
#Region "INotifyPropertyChanged"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Overridable Sub OnPropertyChanged(<CallerMemberName> ByVal Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
#End Region
    End Class
End Namespace
