using HKMMW.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace HKMMW.Views;

public sealed partial class ModGroupsPage : Page
{
    public ModGroupsViewModel ViewModel
    {
        get;
    }

    public ModGroupsPage()
    {
        ViewModel = App.GetService<ModGroupsViewModel>();
        InitializeComponent();
    }
}
