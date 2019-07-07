using System.Collections.Generic;
using WaitForDocker.HealthCheckers;
using WaitForDocker.Logger;

namespace WaitForDocker.Config
{
    public class WaitForDockerComposeConfig
    {
        internal WaitForDockerComposeConfig()
        {
            
        }
        internal string DockerComposeDirPath { get; set; }
        internal string ComposeFileName { get; set; } = DockerConsts.DefaultDockerComposeFile;
        internal int ServiceTimeoutInSeconds { get; set; } = 10;
        internal string[] ComposeParams { get; set; } = {};
        internal ILogger Logger { get; set; } = new DefaultLogger();
        internal bool ThrowOnServiceUnavailability { get; set; } = true;
        internal List<DockerServiceHealthCheck> HealthChecks { get; set; } = new List<DockerServiceHealthCheck>();
    }
}