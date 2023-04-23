using HKMMW.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace HKMMW.Views;

public sealed partial class TasksPage : Page
{
    public TasksViewModel ViewModel
    {
        get;
    }

    public TasksPage()
    {
        ViewModel = App.GetService<TasksViewModel>();
        InitializeComponent();
    }
}
