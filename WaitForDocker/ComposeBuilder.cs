using System.Text;

namespace WaitForDocker
{
    public static class ComposeBuilder
    {
        private const string ComposeUp = "docker-compose up";
        private const string ChangeDirectory = "cd";

        public static string BuildComposeCommand(string dockerComposeDirPath)
        {
            var cmd = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(dockerComposeDirPath))
            {
                cmd.Append($@"{ChangeDirectory} {dockerComposeDirPath} && ");
            }

            cmd.Append(ComposeUp);
            return cmd.ToString();
        }
    }
}