<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/872725720/24.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1258387)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# WPF MVVM Framework - Utilize the ISupportParentViewModel Interface

This example uses the [`ISupportParentViewModel`](https://docs.devexpress.com/WPF/17449/mvvm-framework/viewmodels/viewmodel-relationships-isupportparentviewmodel) interface (exposes a parent view model to its child view models). This interface is implemented automatically across all [`ViewModelBase`](https://docs.devexpress.com/WPF/17351/mvvm-framework/viewmodels/viewmodelbase) descendants. 

## Implementation Details

Use the `ViewModelExtensions.ParentViewModel` attached property to associate a child view model with a parent view model. In XAML, specify the `ParentViewModel` property for the child view as follows so it can access services and data from the parent view model:

```xaml
<local:ChildView 
    dxmvvm:ViewModelExtensions.ParentViewModel="{Binding DataContext, ElementName=LayoutRoot}"/>
```

> Note: The `ViewModelExtensions.ParentViewModel` attached property is set **after** the child view is initialized because the property is not available in a child view constructor.

The [`ISupportParentViewModel`](https://docs.devexpress.com/WPF/17449/mvvm-framework/viewmodels/viewmodel-relationships-isupportparentviewmodel) interface allows a child view model to access MVVM services defined at the parent level:

```cs
IMessageBoxService MessageBoxService {
    get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); }
}
```

Override the `OnParentViewModelChanged` method in a `ViewModelBase` descendant only if your child view model implementation includes custom parent view model changes.

```cs
protected override void OnParentViewModelChanged(object parentViewModel) {
    RaisePropertyChanged(nameof(ISupportParentViewModel.ParentViewModel));
}
```

## Manual Implementation

If your view model does not inherit from `ViewModelBase`, you can implement the `ISupportParentViewModel` interface manually.

The following example implements the `ISupportParentViewModel` interface along with `ISupportServices` and `INotifyPropertyChanged`:

```cs
public class CustomChildViewModel : ISupportParentViewModel, ISupportServices, INotifyPropertyChanged {
    public object ParentViewModel {
        get => _parentViewModel;
        set {
            if (Equals(value, _parentViewModel)) return;
            _parentViewModel = value;
            OnPropertyChanged();
        }
    }

    IMessageBoxService MessageBoxService {
        get => GetService<IMessageBoxService>(ServiceSearchMode.PreferParents);
    }

    protected virtual T GetService<T>(ServiceSearchMode searchMode) where T : class {
        return this.ServiceContainer.GetService<T>(searchMode);
    }

    protected IServiceContainer ServiceContainer {
        get {
            if (serviceContainer == null)
                serviceContainer = new ServiceContainer(this);
            return serviceContainer;
        }
    }

    IServiceContainer ISupportServices.ServiceContainer => ServiceContainer;

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private object _parentViewModel;
    private IServiceContainer serviceContainer;
}
```

## Files to Review

- [MainView.xaml (CS)](.CS/SupportParentViewModel/Views/MainView.xaml) / [MainView.xaml (VB)](.VB/SupportParentViewModel/Views/MainView.xaml)
- [ChildViewModel.cs](./CS/SupportParentViewModel/ViewModels/ChildViewModel.cs) / [ChildViewModel.vb](./VB/SupportParentViewModel/ViewModels/ChildViewModel.vb)
- [CustomChildViewModel.cs](./CS/SupportParentViewModel/ViewModels/CustomChildViewModel.cs) / [CustomChildViewModel.vb](./VB/SupportParentViewModel/ViewModels/CustomChildViewModel.vb)

## Documentation

- [ViewModel relationships (ISupportParentViewModel)](https://docs.devexpress.com/WPF/17449/mvvm-framework/viewmodels/viewmodel-relationships-isupportparentviewmodel)
- [Services](https://docs.devexpress.com/WPF/17414/mvvm-framework/services)
- [View Models](https://docs.devexpress.com/WPF/17439/mvvm-framework/viewmodels)

## More Examples

- [WPF MVVM Framework - Use View Models Generated at Compile Time](https://github.com/DevExpress-Examples/wpf-mvvm-framework-view-model-generator)
- [WPF Dock Layout Manager - Bind the View Model Collection with LayoutAdapters](https://github.com/DevExpress-Examples/wpf-docklayoutmanager-bind-view-model-collection-with-layoutadapters)
- [WPF Dock Layout Manager - Bind the View Model Collection with IMVVMDockingProperties](https://github.com/DevExpress-Examples/wpf-docklayoutmanager-bind-view-model-collection-with-IMVVMDockingProperties)
- [WPF Dock Layout Manager - Populate a DockLayoutManager LayoutGroup with the ViewModels Collection](https://github.com/DevExpress-Examples/wpf-docklayoutmanager-display-viewmodels-collection-in-layoutgroup)
- [Add the Loading Decorator to the Application with the MVVM Structure](https://github.com/DevExpress-Examples/wpf-display-loading-decorator-in-manual-mode-with-mvvm)
- [WPF Hamburger Menu Control - Navigate Between the MVVM Views](https://github.com/DevExpress-Examples/wpf-hamburger-menu-with-mvvm)
- [Reporting for WPF - How to Use ViewModel Data as Report Parameters in a WPF MVVM Application](https://github.com/DevExpress-Examples/reporting-wpf-mvvm-viewmodel-data-to-report)
- [Reporting for WPF - How to Use the DocumentPreviewControl in a WPF MVVM Application to Preview a Report](https://github.com/DevExpress-Examples/reporting-wpf-mvvm-show-report-document-preview)

<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-isupportparentviewmodel&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-isupportparentviewmodel&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
