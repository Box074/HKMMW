using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKMMW.Core.Contracts.Tasks;
public class TaskMessage
{
    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
    public string Text
    {
        get;
    }
    public MessageType Type
    {
        get;
    }
    public TaskMessage(MessageType type, string text)
    {
        Type = type;
        Text = text;
    }

}
public interface ITaskItem : INotifyPropertyChanged
{
    string Name
    {
        get; set;
    }
    string GUID
    {
        get;
    }
    Task Task
    {
        get; set;
    }
    bool IsCompleted => Task.IsCompleted;
    bool ProgressIndeterminate => Progress == -1;
    bool IsError => Task.Exception != null && IsCompleted;
    bool IsSucceeded => Task.IsCompleted && !IsError;
    bool IsRunning => !IsCompleted;
    int Progress
    {
        get; set;
    }
    bool IsHidden
    {
        get; set;
    }
    IList<TaskMessage> Messages
    {
        get;
    }

    void Log(string text, TaskMessage.MessageType type);
    void Log(string text) => Log(text, TaskMessage.MessageType.Info);
    void LogWarning(string text) => Log(text, TaskMessage.MessageType.Warning);
    void LogError(string text) => Log(text, TaskMessage.MessageType.Error);
}
