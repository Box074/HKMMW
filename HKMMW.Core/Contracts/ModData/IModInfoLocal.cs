namespace HKMMW.Core.Contracts.ModData;

public interface IModInfoLocal : IModInfo
{
    long DownloadDate
    {
        get;
    }
    string LocalModRoot
    {
        get;
    }
    IModInfo LatestVersion
    {
        get;
    }
}
