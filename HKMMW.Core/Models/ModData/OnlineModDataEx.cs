using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKMMW.Core.Contracts.ModData;
using Newtonsoft.Json;

namespace HKMMW.Core.Models.ModData;

public class OnlineModDataEx : IModInfo, IModInfoDownloadable
{
    [JsonProperty("link")]
    public string DownloadLink
    {
        get; set;
    }
    [JsonProperty("repository")]
    public string Repository
    {
        get; set;
    }
    public string ModPackageHash => Files.ModSHA;

    [JsonProperty("name")]
    public string Name
    {
        get; set;
    }

    [JsonProperty("desc")]
    public string Description
    {
        get; set;
    }
    [JsonProperty("version")]
    public string VerString
    {
        get; set;
    }
    public Version Version => Version.TryParse(VerString, out var result) ? result : new();

    public string ModId => Name;

    [JsonProperty("dependencies")]
    public List<string> Dependencies { get; set; } = new();
    
    IList<string> IModInfo.Dependencies => Dependencies;

    [JsonProperty("integrations")]
    public List<string> Integrations { get; set; } = new();

    IList<string> IModInfo.Integrations => Integrations;

    public class ModFileRecord
    {
        [JsonProperty("files")]
        public Dictionary<string, string> FileSHA = new();
        [JsonProperty("size")]
        public long ModSize = 0;
        [JsonProperty("sha256")]
        public string ModSHA = "";
    }

    [JsonProperty("ei_files")]
    public ModFileRecord Files
    {
        get; set;
    } = new();

    public long ModSize => Files.ModSize;
}
