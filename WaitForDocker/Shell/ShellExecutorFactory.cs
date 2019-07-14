using WaitForDocker.Logger;

namespace WaitForDocker.Shell
{
    internal static class ShellExecutorFactory
    {
        public static ShellExecutor GetShellExecutor(ILogger logger)
        {
            var shell = ShellFactory.GetShell();
            return new ShellExecutor(shell, logger);
        }
    }
}