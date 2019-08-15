using System;
using System.Collections.Generic;
using WaitForDocker.HealthCheckers;
using WaitForDocker.Logger;

namespace WaitForDocker.Config
{
    public class WaitForDockerConfigurationBuilder
    {
        private readonly WaitForDockerConfig config = new WaitForDockerConfig();
        private List<Func<DockerHealthCheckFactory, DockerHealthChecker>> HealthChecksFuncs { get; } = new List<Func<DockerHealthCheckFactory, DockerHealthChecker>>();

        public WaitForDockerConfigurationBuilder SetComposeDirectoryPath(string path)
        {
            config.DockerComposeDirPath = path;
            return this;
        }

        public WaitForDockerConfigurationBuilder SetCustomComposeFileName(string fileName)
        {
            config.ComposeFileName = fileName;
            return this;
        }

        public WaitForDockerConfigurationBuilder SetComposeParams(IEnumerable<string> composeParams)
        {
            config.ComposeParams.AddRange(composeParams);
            return this;
        }

        public WaitForDockerConfigurationBuilder SetCustomLogger(ILogger logger)
        {
            config.Logger = logger;
            return this;
        }

        public WaitForDockerConfigurationBuilder SetRenewAnonVolumes(bool renew)
        {
            config.RenewAnonVolumes = renew;
            return this;
        }

        public WaitForDockerConfigurationBuilder SetProjectName(string projectName)
        {
            config.DockerComposeProjectName = projectName;
            return this;
        }

        public WaitForDockerConfigurationBuilder AddHealthCheck(Func<DockerHealthCheckFactory, DockerHealthChecker> serviceCheckerFunc)
        {
            HealthChecksFuncs.Add(serviceCheckerFunc);
            return this;
        }

        public WaitForDockerConfig Build()
        {
            foreach (var healthChecksFunc in HealthChecksFuncs)
            {
                config.HealthCheckers.Add(healthChecksFunc(new DockerHealthCheckFactory(config)));
            }
            return config;
        }
    }
}
