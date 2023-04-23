using HKMMW.Contracts.Services;
using HKMMW.Core.Contracts.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HKMMW.Helpers;

public class TaskMessageTypeToBrush : IValueConverter
{
    public TaskMessageTypeToBrush()
    {
    }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is TaskMessage.MessageType type)
        {
            var theme = App.GetService<IThemeSelectorService>().Theme;

            var color = theme switch
            {
                ElementTheme.Dark => type switch
                {
                    TaskMessage.MessageType.Error => Color.FromArgb(255, 0xff, 0x99, 0xa4),
                    TaskMessage.MessageType.Warning => Color.FromArgb(255, 0xfc, 0xe1, 0x00),
                    _ => Colors.White
                },
                _ => type switch
                {
                    TaskMessage.MessageType.Error => Color.FromArgb(255, 0xc4, 0x2b, 0x1c),
                    TaskMessage.MessageType.Warning => Color.FromArgb(255, 0x9d, 0x5d, 0x00),
                    _ => Colors.Black
                }
            };
            return new SolidColorBrush(color);

        }

        throw new NotSupportedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
