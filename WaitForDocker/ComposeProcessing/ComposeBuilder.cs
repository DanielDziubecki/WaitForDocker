using System.Text;
using WaitForDocker.Config;

namespace WaitForDocker.ComposeProcessing
{
    public static class ComposeBuilder
    {
        private const string ComposeUp = "docker-compose {0} up -d --no-color";
        private const string ComposeKill = "docker-compose kill";
        private const string ChangeDirectory = "cd";

        public static string BuildComposeCommand(WaitForDockerComposeConfig composeConfig)
        {
            var cmd = new StringBuilder();
            var changeDirCommand = GetChangeDirCommand(composeConfig.DockerComposeDirPath);

            cmd.Append(changeDirCommand);
            cmd.Append(string.Format(ComposeUp, string.Join(" ", composeConfig.ComposeParams)));
            return cmd.ToString();
        }

        public static string BuildComposeKillCommand(WaitForDockerComposeKillConfig composeKillConfig)
        {
            var cmd = new StringBuilder();
            var changeDirCommand = GetChangeDirCommand(composeKillConfig.DockerComposeDirPath);
            cmd.Append(changeDirCommand);

            cmd.Append(ComposeKill);
            return cmd.ToString();
        }

        private static string GetChangeDirCommand(string dockerComposeDirPath)
           => !string.IsNullOrWhiteSpace(dockerComposeDirPath) ? $@"{ChangeDirectory} {dockerComposeDirPath} && " : string.Empty;
    }
}