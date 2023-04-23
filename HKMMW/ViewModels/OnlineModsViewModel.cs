using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HKMMW.Core.Contracts.ModData;
using HKMMW.Core.Contracts.Services;

namespace HKMMW.ViewModels;

public class OnlineModsViewModel : ObservableRecipient
{
    public ObservableCollection<IModInfo> mods = new();
    private readonly IModLinksModInfoProvideService modlinks;
    public OnlineModsViewModel(IModLinksModInfoProvideService modLinksModInfoProvideService)
    {
        modlinks = modLinksModInfoProvideService;

        _ = RefreshModInfos();
    }

    public async Task RefreshModInfos()
    {
        mods.Clear();
        foreach (var mod in await modlinks.GetLatestModInfos())
        {
            mods.Add(mod);
        }
    }
}
