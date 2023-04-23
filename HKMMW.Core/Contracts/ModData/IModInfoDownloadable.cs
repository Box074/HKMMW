namespace HKMMW.Core.Contracts.ModData;

public interface IModInfoDownloadable : IModInfo
{
    string DownloadLink
    {
        get;
    }
    string ModPackageHash
    {
        get;
    }
    long ModSize
    {
        get;
    }
    
    string HashType => "sha256";
}
