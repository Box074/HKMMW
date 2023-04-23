using HKMMW.Core.Contracts.ModData;

namespace HKMMW.Core.Contracts.Services;

public interface IModInfoProvideService
{
    Task<IList<IReadOnlyDictionary<string, IModInfo>>> GetModInfos();
    Task<IReadOnlyDictionary<string, IModInfo>> GetModInfo(string modid);
    void ClearCache()
    {
    }

    Task<IModInfo> GetLatestMod(string modid);
    Task<IList<IModInfo>> GetLatestModInfos();
}
