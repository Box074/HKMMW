using CommunityToolkit.Mvvm.ComponentModel;
using HKMMW.Core.Contracts.Services;
using HKMMW.Core.Contracts.Tasks;
using HKMMW.Core.Models.Tasks;

namespace HKMMW.ViewModels;

public class TasksViewModel : ObservableRecipient
{
    private readonly ITaskService taskService;
    public IEnumerable<ITaskItem>? tasks;
    public TasksViewModel(ITaskService taskService)
    {
        this.taskService = taskService;
        tasks = taskService.GetTasks();

    }

}
