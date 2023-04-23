using HKMMW.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace HKMMW.Views;

public sealed partial class LocalModsPage : Page
{
    public LocalModsViewModel ViewModel
    {
        get;
    }

    public LocalModsPage()
    {
        ViewModel = App.GetService<LocalModsViewModel>();
        InitializeComponent();
    }
}
