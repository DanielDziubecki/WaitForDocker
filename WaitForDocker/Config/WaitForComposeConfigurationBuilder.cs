using System;
using WaitForDocker.HealthCheckers;
using WaitForDocker.Logger;

namespace WaitForDocker.Config
{
    public class WaitForComposeConfigurationBuilder
    {
        private readonly WaitForDockerComposeConfig config = new WaitForDockerComposeConfig();

        public WaitForComposeConfigurationBuilder SetComposeDirectoryPath(string path)
        {
            config.DockerComposeDirPath = path;
            return this;
        }

        public WaitForComposeConfigurationBuilder SetCustomComposeFileName(string fileName)
        {
            config.ComposeFileName = fileName;
            return this;
        }

        public WaitForComposeConfigurationBuilder SetServiceHealthCheckTimeout(int timeout)
        {
            config.ServiceTimeoutInSeconds = timeout;
            return this;
        }

        public WaitForComposeConfigurationBuilder SetComposeRunParams(string[] composeParams)
        {
            config.ComposeParams = composeParams;
            return this;
        }

        public WaitForComposeConfigurationBuilder SetCustomLogger(ILogger logger)
        {
            config.Logger = logger;
            return this;
        }

        public WaitForComposeConfigurationBuilder ThrowOnServiceUnavailability(bool @throw)
        {
            config.ThrowOnServiceUnavailability = @throw;
            return this;
        }

        public WaitForComposeConfigurationBuilder AddDockerServiceHealthCheck(string serviceName, Func<HealthCheckFactory ,IServiceHealthChecker> serviceCheckerFunc)
        {
            config.HealthChecks.Add(new DockerServiceHealthCheck(serviceName, serviceCheckerFunc(new HealthCheckFactory())));
            return this;
        }

        public WaitForDockerComposeConfig Build()
        {
            return config;
        }
    }
}
