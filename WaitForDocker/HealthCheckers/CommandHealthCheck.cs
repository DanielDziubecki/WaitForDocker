using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WaitForDocker.Shell;

namespace WaitForDocker.HealthCheckers
{
    internal class CommandHealthCheck : IServiceHealthChecker
    {
        private readonly string command;
        private readonly IShell shell;

        public CommandHealthCheck(string command, IShell shell)
        {
            this.command = command;
            this.shell = shell;
        }

        public Task<bool> IsHealthy()
        {
            var startInfo = GetProcessStartInfo();
            using (var process = Process.Start(startInfo))
            {
                //process.WaitForExit();
                //_logger.Log(process.StandardOutput.ReadToEnd());
                //_logger.Log(process.StandardError.ReadToEnd());
                //_logger.Log($"Process finished with exit code: {process.ExitCode.ToString()}");
                //_logger.Log($"Command {commandType} finished");
                //_logger.Log(string.Empty);
            }

            return Task.FromResult(true);
        }

        private ProcessStartInfo GetProcessStartInfo()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = shell.GetFileName(),
                Arguments = shell.CommandConstructor(command),
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardErrorEncoding = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8
            };
            return startInfo;
        }
    }
}