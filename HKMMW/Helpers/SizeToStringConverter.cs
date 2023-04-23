using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace HKMMW.Helpers;

public class SizeToStringConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not long size)
        {
            throw new NotSupportedException();
        }
        if (size > 1024 * 1024 * 1024)
        {
            return (size / 1024 / 1024 / 1024) + "GB";
        }
        else if (size > 1024 * 1024)
        {
            return (size / 1024 / 1024) + "MB";
        }
        else if (size > 1024)
        {
            return (size / 1024) + "KB";
        }
        else
        {
            return size + "B";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
