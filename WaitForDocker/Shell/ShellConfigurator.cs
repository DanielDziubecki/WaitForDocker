using System;
using System.Diagnostics;
using System.Text;

namespace WaitForDocker.Shell
{
    public sealed class ShellConfigurator
    {
        private readonly ILogger _logger;
        private static IShell _shell;

        public ShellConfigurator(IShell shell, ILogger logger)
        {
            _logger = logger;
            _shell = shell ?? throw new ArgumentException(nameof(shell));

            if (!OS.IsWin())
            {
                Execute("chmod +x cmd.sh");
            }
        }

        public void Execute(string command)
        {
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            var startInfo = new ProcessStartInfo
            {
                FileName = _shell.GetFileName(),
                Arguments = _shell.CommandConstructor(command),
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow =true
            };

            _logger.Log("*** Compose command started executing ***");
            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                _logger.Log("*** Process finished  ***");
                _logger.Log($"*** Standard output: {stdout.ToString()}  ***");
                _logger.Log($"*** Standard error: {stderr.ToString()}  ***");
                _logger.Log($"*** Exit code: { process.ExitCode}  ***");

            }

        }
    }
}