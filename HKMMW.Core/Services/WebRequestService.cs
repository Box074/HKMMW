using System.Net;
using System.Text;
using System.Threading.Tasks;
using HKMMW.Core.Contracts.Services;
using HKMMW.Core.Contracts.Tasks;
using System;
using System.Net.Http.Handlers;

namespace HKMMW.Core.Services;
public class WebRequestService : IWebRequestService
{
    public readonly int bufferSize = 8192;
    public async Task<byte[]> FetchBinaryAsync(string url, ITaskItem task = null)
    {
        using var pmh = new ProgressMessageHandler(new HttpClientHandler());
        
        if (task is not null)
        {
            var ctx = SynchronizationContext.Current;
            pmh.HttpReceiveProgress += (object sender, HttpProgressEventArgs args) =>
            {
                if (args.TotalBytes is not null)
                {
                    ctx.Post(_ =>
                    {
                        var progress = args.BytesTransferred * 100 / args.TotalBytes;
                        task.Progress = (int)progress;
                        task.Log($"Receive: {progress}% ({args.BytesTransferred}/{args.TotalBytes})");
                    }, null);
                    
                }
            };
        }

        using HttpClient client = new(pmh);

        task?.Log($"Try to fetch {url}");

        using var rep = await client.GetAsync(url);
        rep.EnsureSuccessStatusCode();
        task?.Log($"Status Code: {rep.StatusCode} {rep.ReasonPhrase}");

        var result = await rep.Content.ReadAsByteArrayAsync();
        task?.Log($"The request to {url} is completed");
        return result;
    }
    public async Task<string> FetchTextAsync(string url, Encoding encoding = null, ITaskItem task = null)
    {
        return (encoding ?? Encoding.UTF8).GetString(await FetchBinaryAsync(url));
    }
}
