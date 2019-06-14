namespace WaitForDocker
{
    public class WaitForDockerConfig
    {
        public string DockerComposeDirPath { get; set; }
        public string ComposeFileName { get; set; } = "docker-compose.yaml";
        public int ServiceTimeoutInSeconds { get; set; } = 60;
        public string[] ComposeParams { get; set; }
        public ILogger Logger { get; set; } = new DefaultLogger();
    }
}