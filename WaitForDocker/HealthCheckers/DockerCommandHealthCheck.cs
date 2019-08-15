using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WaitForDocker.ComposeProcessing;
using WaitForDocker.Logger;
using WaitForDocker.Shell;

namespace WaitForDocker.HealthCheckers
{
    internal sealed class DockerCommandHealthCheck : DockerHealthChecker
    {
        private readonly string _composeProjectName;
        private readonly string _command;
        private readonly IShell _shell;
        private const int SuccessExitCode = 0;

        public DockerCommandHealthCheck(string serviceName, string command, int timeoutInSeconds, string composeProjectName, int? portOfDistinction, ILogger logger) :
            base(serviceName, logger, timeoutInSeconds, portOfDistinction)
        {
            _composeProjectName = composeProjectName;
            _command = command;
            _shell = ShellFactory.GetShell();
        }

        public override async Task<bool> IsHealthy()
        {
            var fullDockerCommand = DockerCommandBuilder.BuildDockerExecCommand(_composeProjectName, ServiceName, _command);
            Logger.Log($"Command health check of {ServiceName} with command {fullDockerCommand} has been started.");
            var startInfo = GetProcessStartInfo(fullDockerCommand);
            var sp = new Stopwatch();
            var attempts = 1;

            while (sp.Elapsed.Seconds < TimeoutInSeconds)
            {
                sp.Start();
                using (var process = Process.Start(startInfo))
                {
                    if (process == null)
                        throw new WaitForDockerException("Unable to create process");

                    process.WaitForExit();
                    var processExitCode = process.ExitCode;
                    Logger.Log($"Attempt number {attempts} of {ServiceName} command health check exited with  exit code: {processExitCode.ToString()} and following output \r\n " +
                               $"Output: \r\n {process.StandardOutput.ReadToEnd()} \r\n" +
                               $"Errors: \r\n{process.StandardError.ReadToEnd()}");
                    Logger.Log(string.Empty);

                    if (processExitCode == SuccessExitCode)
                    {
                        Logger.Log($"Command health check of {ServiceName} return success exit code {processExitCode.ToString()}");
                        return true;
                    }

                }

                sp.Stop();
                await Task.Delay(100);
                attempts++;
            }

            var exceptionMessage = $"Health check failed! Service {ServiceName} was not returning success http status code after {TimeoutInSeconds} seconds.";
            Logger.Log(exceptionMessage);

            throw new WaitForDockerException(exceptionMessage);
        }

        private ProcessStartInfo GetProcessStartInfo(string fullDockerCommand)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _shell.GetFileName(),
                Arguments = _shell.CommandConstructor(fullDockerCommand),
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