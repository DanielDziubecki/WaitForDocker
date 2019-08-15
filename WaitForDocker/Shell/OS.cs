using System;
using System.Runtime.InteropServices;

namespace WaitForDocker.Shell
{
    internal static class Os
    {
        public static bool IsWin() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMac() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsGnu() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static OSPlatform GetCurrent()
        {
            return IsWin() ? OSPlatform.Windows : 
                   IsMac() ? OSPlatform.OSX :
                   IsGnu() ? OSPlatform.Linux : 
                   throw new NotSupportedException("Not suported OS");
        }
    }
}