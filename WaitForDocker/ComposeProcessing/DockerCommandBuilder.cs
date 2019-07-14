using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WaitForDocker.Config;

namespace WaitForDocker.ComposeProcessing
{
    internal static class DockerCommandBuilder
    {
        private const string ComposeUp = "docker-compose {0} up -d --no-color";
        private const string DockerKill = "docker kill {0}";
        private const string ChangeDirectory = "cd";

        internal static string BuildComposeCommand(WaitForDockerConfig config)
        {
            var cmd = new StringBuilder();
            var changeDirCommand = GetChangeDirCommand(config.DockerComposeDirPath);

            config.ComposeParams.Add($"{DockerConsts.DockerComposeProjectNameParam} {config.DockerComposeProjectName}");
            cmd.Append(changeDirCommand);
            cmd.Append(string.Format(ComposeUp, string.Join(" ", config.ComposeParams)));
            return cmd.ToString();
        }

        internal static string BuildDockerKillCommand(WaitForDockerConfig config, IEnumerable<ServicePort> servicePorts)
        {
            var cmd = new StringBuilder();
            var changeDirCommand = GetChangeDirCommand(config.DockerComposeDirPath);
            cmd.Append(changeDirCommand);
            cmd.Append(string.Join(" && ", servicePorts.Select(x=>x.Name).Distinct().Select(x => string.Format(DockerKill, $"{config.DockerComposeProjectName}_{x}_1"))));
            return cmd.ToString();
        }

        internal static string BuildDockerExecCommand(string composeProjectName, string serviceName, string command)
        {
            var containerName = $"{composeProjectName}_{serviceName}_{DockerConsts.DockerComposeNumberOfInstances}".ToLower(CultureInfo.InvariantCulture);
            return $"{DockerConsts.DockerExec} {containerName} {command}";
        }

        internal static string GetChangeDirCommand(string dockerComposeDirPath)
           => !string.IsNullOrWhiteSpace(dockerComposeDirPath) ? $@"{ChangeDirectory} {dockerComposeDirPath} && " : string.Empty;
    }
}