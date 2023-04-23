using System.Collections.ObjectModel;
using System.ComponentModel;
using HKMMW.Core.Contracts.Tasks;

namespace HKMMW.Core.Models.Tasks;


public class TaskItem : ITaskItem
{
    public Task Task
    {
        get; set;
    }

    public int Progress
    {
        get; set;
    } = -1;

    public bool IsHidden
    {
        get; set;
    } = false;

    public string Name
    {
        get; set;
    } = "Task Name";

    public string GUID
    {
        get;
    } = Guid.NewGuid().ToString();

    public bool ProgressIndeterminate => Progress < 0;
    public bool IsError => Task.Exception != null && IsCompleted;
    public bool IsSucceeded => Task.IsCompleted && !IsError;
    public bool IsRunning => !IsCompleted;
    public bool IsCompleted
    {
        get; internal set;
    }
    private readonly ObservableCollection<TaskMessage> messages = new();

    public IList<TaskMessage> Messages => messages;

    public event PropertyChangedEventHandler PropertyChanged;

    

    public void Log(string text, TaskMessage.MessageType type)
    {
        messages.Insert(0, new(type, string.Join('\n', 
            text.Split('\n')
            .Select(x => $"[{DateTime.Now}][{type}] - {x}")
            .ToArray())));
    }
}
