using HKMMW.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace HKMMW.Views;

public sealed partial class MAPIManagePage : Page
{
    public MAPIManageViewModel ViewModel
    {
        get;
    }

    public MAPIManagePage()
    {
        ViewModel = App.GetService<MAPIManageViewModel>();
        InitializeComponent();
    }
}
