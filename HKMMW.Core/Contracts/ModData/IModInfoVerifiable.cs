namespace HKMMW.Core.Contracts.ModData;

public interface IModInfoVerifiable : IModInfo
{
    IDictionary<string, string> ModFileSHAs
    {
        get;
    }
}
