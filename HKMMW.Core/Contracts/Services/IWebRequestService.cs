using System.Text;
using HKMMW.Core.Contracts.Tasks;

namespace HKMMW.Core.Contracts.Services;
public interface IWebRequestService
{
    Task<byte[]> FetchBinaryAsync(string url, ITaskItem task = null);
    Task<string> FetchTextAsync(string url, Encoding encoding = null, ITaskItem task = null);
}
