using System;
using System.Runtime.InteropServices;

namespace WaitForDocker.Shell
{
    internal static class ShellFactory
    {
        public static IShell GetShell()
        {
            IShell shell;
            var osPlatform = OS.GetCurrent();

            if (osPlatform == OSPlatform.Windows)
            {
                shell = ShellType.Bat;
            }
            else if (osPlatform == OSPlatform.Linux || osPlatform == OSPlatform.OSX)
            {
                shell = ShellType.Bash;
            }
            else
            {
                throw new NotSupportedException("Not supported os type");
            }

            return shell;
        }
    }
}