using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HKMMW.Core.Contracts.ModData;
using HKMMW.Core.Contracts.Services;
using HKMMW.Helpers;
using Microsoft.UI.Xaml;

namespace HKMMW.ViewModels;

public class OnlineModsViewModel : ObservableRecipient
{
    public IEnumerable<IModInfo>? mods;
    private readonly IModLinksModInfoProvideService modlinks;
    private readonly ITaskService taskService;
    public OnlineModsViewModel(IModLinksModInfoProvideService modLinksModInfoProvideService,
            ITaskService taskService)
    {
        modlinks = modLinksModInfoProvideService;
        this.taskService = taskService;

        RefreshModInfos().RecordError();
    }

    public async Task RefreshModInfos()
    {
        var results = await modlinks.GetLatestModInfos();
        taskService.AppContext.Post(_ =>
        {
            SetProperty(ref mods, results, nameof(mods));
        }, null);
    }
}
