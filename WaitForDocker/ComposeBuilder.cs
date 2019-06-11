using System.Text;

namespace WaitForDocker
{
    public static class ComposeBuilder
    {
        private const string ComposeUp = "docker-compose {0} up";
        private const string ChangeDirectory = "cd";

        public static string BuildComposeCommand(WaitForDockerConfig config)
        {
            var cmd = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(config.DockerComposeDirPath))
            {
                cmd.Append($@"{ChangeDirectory} {config.DockerComposeDirPath} && ");
            }

            cmd.Append(string.Format(ComposeUp, string.Join(" ", config.ComposeParams)));
            return cmd.ToString();
        }
    }
}