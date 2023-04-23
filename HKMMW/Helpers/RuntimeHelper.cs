using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace HKMMW.Helpers;

public static class RuntimeHelper
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder? packageFullName);

    public static bool IsMSIX
    {
        get
        {
            var length = 0;

            return GetCurrentPackageFullName(ref length, null) != 15700L;
        }
    }

    public static async void RecordError(this Task task)
    {
        try
        {
            await task;
        } catch(Exception ex)
        {
            Debugger.Break();
        }
    }

}
