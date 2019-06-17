using WaitForDocker.Logger;

namespace WaitForDocker.Config
{
    public class WaitForDockerComposeKillConfig
    {
        public string DockerComposeDirPath { get; set; }
        public ILogger Logger { get; set; } = new DefaultLogger();
    }
}