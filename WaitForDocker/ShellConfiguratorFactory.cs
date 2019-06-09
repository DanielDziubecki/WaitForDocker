using System;
using System.Runtime.InteropServices;
using WaitForDocker.Notification;
using WaitForDocker.Shell;

namespace WaitForDocker
{
    public static class ShellConfiguratorFactory
    {
        public static ShellConfigurator GetShell(IShellOutputWriter outputWriter = null)
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

            return new ShellConfigurator(shell, ShellOutputWriter.Default);
        }
    }
}