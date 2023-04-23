using System.Collections.ObjectModel;
using System.ComponentModel;
using HKMMW.Core.Contracts.Services;
using HKMMW.Core.Contracts.Tasks;
using HKMMW.Core.Models.Tasks;

namespace HKMMW.Core.Services;
public class TaskService : ITaskService
{
    private readonly ObservableCollection<ITaskItem> tasks = new();

    public SynchronizationContext AppContext
    {
        get; set;
    }

    public void AddTask(ITaskItem task)
    {
        if (tasks.Contains(task))
        {
            return;
        }
        if (task is TaskItem ti)
        {
            async Task WaitForEnd(TaskItem t)
            {
                try
                {
                    await t.Task;
                    AppContext.Post(_ =>
                    {
                        t.Progress = 100;
                    }, null);
                }
                catch(Exception ex)
                {
                    AppContext.Post(_ =>
                    {
                        t.Log(ex.ToString(), TaskMessage.MessageType.Error);
                    }, null);
                }
                finally
                {
                    AppContext.Post(_ =>
                    {
                        t.IsCompleted = true;
                    }, null);
                }
            }
            _ = WaitForEnd(ti);
        }
        tasks.Insert(0, task);
    }
    public ITaskItem AddTask(Task task)
    {
        var result = new TaskItem()
        {
            Task = task,
        };
        AddTask(result);
        return result;
    }
    public ITaskItem AddTask(Func<ITaskItem, Task> task)
    {
        var result = new TaskItem();
        var prev = SynchronizationContext.Current;
        try
        {

            SynchronizationContext.SetSynchronizationContext(AppContext);
            result.Task = task(result);

            AddTask(result);
            return result;
        }
        finally
        {
            SynchronizationContext.SetSynchronizationContext(prev);
        }
    }
    public Task<T> AddTask<T>(Func<ITaskItem, Task<T>> task, out ITaskItem taskItem)
    {
        var result = new TaskItem();
        var prev = SynchronizationContext.Current;
        try
        {

            SynchronizationContext.SetSynchronizationContext(AppContext);
            result.Task = task(result);
            taskItem = result;
            AddTask(result);
            return (Task<T>)result.Task;
        }
        finally
        {
            SynchronizationContext.SetSynchronizationContext(prev);
        }
    }

    public IEnumerable<ITaskItem> GetTasks()
    {
        return tasks;
    }

    public void RemoveTask(ITaskItem task)
    {
        tasks.Remove(task);
    }

    public TaskService()
    {
        AppContext = SynchronizationContext.Current;
    }
}
