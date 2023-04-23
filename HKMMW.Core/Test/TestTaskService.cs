using HKMMW.Core.Contracts.Services;
using HKMMW.Core.Contracts.Tasks;
using HKMMW.Core.Models.Tasks;
using HKMMW.Core.Services;

namespace HKMMW.Core.Test;
public class TestTaskService : TaskService
{
    private int count = 0;
    private readonly IWebRequestService web;
    private async Task TestTask(ITaskItem task)
    {
        task.Name = "Test" + count++;
        var i = 0;
        while (true)
        {
            task.Progress = i++;
            if (i > 100)
            {
                AddTask(TestTask);
                if (count % 2 == 0) throw null;
                return;
            }
            await Task.Delay(50);
            task.Log("ADADAW3\nTest " + i);

        }
    }
    private async Task TestDownload(ITaskItem task)
    {
        task.Name = "Download";

        await web.FetchBinaryAsync(@"https://github.com/PrashantMohta/HollowKnight.CustomKnight/releases/download/v2.2.0/DefaultCinematics.zip",
            task);
        await web.FetchBinaryAsync(@"https://github.com/PrashantMohta/HollowKnight.CustomKnight/releases/download/v2.2.0/CustomKnight.zip",
            task);
    }

    public TestTaskService(IWebRequestService webRequestService)
    {
        web = webRequestService;

        AddTask(TestTask);
        AddTask(TestDownload);
    }
}
