namespace WaitForDocker
{
    public class WaitForDockerComposeConfig
    {
        public string DockerComposeDirPath { get; set; }
        public string ComposeFileName { get; set; } = DockerConsts.DefaultDockerComposeFile;
        public int ServiceTimeoutInSeconds { get; set; } = 10;
        public string[] ComposeParams { get; set; } = {};
        public ILogger Logger { get; set; } = new DefaultLogger();
        public bool ThrowOnServiceUnavailability { get; set; } = true;
    }

    public class WaitForDockerComposeKillConfig
    {
        public string DockerComposeDirPath { get; set; }
        public ILogger Logger { get; set; } = new DefaultLogger();
    }
}