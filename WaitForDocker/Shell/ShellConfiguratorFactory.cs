using System;
using System.Runtime.InteropServices;
using WaitForDocker.Logger;

namespace WaitForDocker.Shell
{
    public static class ShellConfiguratorFactory
    {
        public static ShellConfigurator GetShell(ILogger logger)
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

            return new ShellConfigurator(shell, logger);
        }
    }
}