using HKMMW.Core.Contracts.ModData;
using HKMMW.Core.Contracts.Services;
using HKMMW.Core.Models.ModData;
using Newtonsoft.Json.Linq;

namespace HKMMW.Core.Services;
public class ModLinksModInfoProvideService : IModLinksModInfoProvideService
{
    private readonly IWebRequestService webRequest;
    private readonly ITaskService taskService;
    private readonly Dictionary<string, Dictionary<string, IModInfo>> cache = new();
    private bool hasCache = false;
    public ModLinksModInfoProvideService(IWebRequestService webRequestService, ITaskService taskService)
    {
        webRequest = webRequestService;
        this.taskService = taskService;
    }

    private Task FetchModLinks()
    {
        return taskService.AddTask(async task =>
        {
            task.Name = "Fetch ModLinks";
            var modlinksText = await webRequest.FetchTextAsync(@"https://github.com/HKLab/modlinks-archive/raw/master/modlinks.json", null, task);
            hasCache = true;
            var root = JObject.Parse(modlinksText);
            var mods = (JObject)root["mods"];
            cache.Clear();
            foreach (var (name, vers) in mods)
            {
                if (!cache.TryGetValue(name, out var group))
                {
                    group = new();
                    cache.Add(name, group);
                }
                foreach (var (ver, mod) in (JObject)vers)
                {
                    var m = mod.ToObject<OnlineModDataEx>();
                    group[ver] = m;
                }
            }
        }).Task;
    }


    public async Task<IReadOnlyDictionary<string, IModInfo>> GetModInfo(string modid)
    {
        if (!hasCache)
        {
            await FetchModLinks();
        }
        return GetModInfoUnsafe(modid);
    }

    private IReadOnlyDictionary<string, IModInfo> GetModInfoUnsafe(string modid)
    {
        if (cache.TryGetValue(modid, out var result))
        {
            return result;
        }
        return null;
    }

    public void ClearCache()
    {
        cache.Clear();
    }


    public async Task<IList<IReadOnlyDictionary<string, IModInfo>>> GetModInfos()
    {
        if (!hasCache)
        {
            await FetchModLinks();
        }
        
        return cache.Values.OfType<IReadOnlyDictionary<string, IModInfo>>().ToList();
    }
    public async Task<IModInfo> GetLatestMod(string modid)
    {
        if (!hasCache)
        {
            await FetchModLinks();
        }
        return GetLatestModUnsafe(modid);
    }
    private IModInfo GetLatestModUnsafe(string modid)
    {
        var vers = GetModInfoUnsafe(modid);
        IModInfo latest = null;
        foreach (var v in vers)
        {
            if (latest == null || v.Value.Version >= latest.Version)
            {
                latest = v.Value;
            }
        }
        return latest;
    }

    public async Task<IList<IModInfo>> GetLatestModInfos()
    {
        if (!hasCache)
        {
            await FetchModLinks();
        }

        return cache.Keys.Select(GetLatestModUnsafe).ToList();
    }
}
