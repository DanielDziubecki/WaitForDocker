using WaitForDocker.Notification;

namespace WaitForDocker
{
    public class WaitForDockerConfig
    {
        public string DockerComposeDirPath { get; set; }
        public string ComposeFileName { get; set; } = "docker-compose.yaml";
        public int ServiceTimeoutInSeconds { get; set; } = 60;
        public IShellOutputWriter ShellOutputWriter { get; set; } = new DefaultShellOutputWriter();
        public string[] ComposeParams { get; set; }
    }
}