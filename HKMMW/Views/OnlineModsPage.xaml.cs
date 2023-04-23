using HKMMW.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace HKMMW.Views;

public sealed partial class OnlineModsPage : Page
{
    public OnlineModsViewModel ViewModel
    {
        get;
    }

    public OnlineModsPage()
    {
        ViewModel = App.GetService<OnlineModsViewModel>();
        InitializeComponent();
    }
}
