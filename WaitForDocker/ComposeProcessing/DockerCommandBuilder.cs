using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WaitForDocker.Config;

namespace WaitForDocker.ComposeProcessing
{
    internal static class DockerCommandBuilder
    {
        private const string ComposeUp = "docker-compose {0} up -d --no-color {1}";
        private const string DockerKill = "docker kill {0}";
        private const string ChangeDirectory = "cd";
        private const string RenewAnonVolumes = "--renew-anon-volumes";

        internal static string BuildComposeCommand(WaitForDockerConfig config)
        {
            var cmd = new StringBuilder();
            var changeDirCommand = GetChangeDirCommand(config.DockerComposeDirPath);

            if(config.RenewAnonVolumes)
                config.ComposeParams.Add(RenewAnonVolumes);

            cmd.Append(changeDirCommand);
            cmd.Append(string.Format(ComposeUp, $"{DockerConsts.DockerComposeProjectNameParam} {config.DockerComposeProjectName}", string.Join(" ", config.ComposeParams)));
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