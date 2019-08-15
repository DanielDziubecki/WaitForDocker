using System.Collections.Generic;
using WaitForDocker.HealthCheckers;
using WaitForDocker.Logger;

namespace WaitForDocker.Config
{
    public class WaitForDockerConfig
    {
        internal WaitForDockerConfig()
        {

        }

        internal string DockerComposeProjectName { get; set; } = DockerConsts.DockerComposeProjectName;
        internal string DockerComposeDirPath { get; set; }
        internal string ComposeFileName { get; set; } = DockerConsts.DefaultDockerComposeFile;
        internal List<string> ComposeParams { get; set; } = new List<string>();    
        internal ILogger Logger { get; set; } = new DefaultLogger();
        internal List<DockerHealthChecker> HealthCheckers { get; set; } = new List<DockerHealthChecker>();
        internal bool RenewAnonVolumes { get; set; } = true;
    }
}