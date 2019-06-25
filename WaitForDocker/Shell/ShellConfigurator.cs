using System;
using System.Diagnostics;
using System.Text;
using WaitForDocker.Logger;

namespace WaitForDocker.Shell
{
    internal sealed class ShellConfigurator
    {
        private readonly ILogger _logger;
        private readonly IShell _shell;

        public ShellConfigurator(IShell shell, ILogger logger)
        {
            _logger = logger;
            _shell = shell ?? throw new ArgumentException(nameof(shell));
        }

        public void Execute(string command, string commandType = "")
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _shell.GetFileName(),
                Arguments = _shell.CommandConstructor(command),
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardErrorEncoding = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8
            };

            _logger.Log(string.Empty);
            _logger.Log($"Starting {commandType} command..");
            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                _logger.Log(process.StandardOutput.ReadToEnd());
                _logger.Log(process.StandardError.ReadToEnd());
                _logger.Log($"Process finished with exit code: {process.ExitCode.ToString()}");
                _logger.Log($"Command {commandType} finished");
                _logger.Log(string.Empty);
            }
        }
    }
}