using HKMMW.Core.Contracts.Tasks;

namespace HKMMW.Core.Contracts.Services;
public interface ITaskService
{
    SynchronizationContext AppContext
    {
        get; set;
    }
    IEnumerable<ITaskItem> GetTasks();
    void AddTask(ITaskItem task);
    ITaskItem AddTask(Task task);
    ITaskItem AddTask(Func<ITaskItem, Task> task);
    void RemoveTask(ITaskItem task);
    Task<T> AddTask<T>(Func<ITaskItem, Task<T>> task, out ITaskItem taskItem);
}
