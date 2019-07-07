using WaitForDocker.Logger;

namespace WaitForDocker.Config
{
    public class WaitForDockerComposeKillConfig
    {
        internal string DockerComposeDirPath { get; set; }
        internal ILogger Logger { get; set; } = new DefaultLogger();
    }
}