using System;
using WaitForDocker.Config;

namespace WaitForDocker.HealthCheckers
{
    public sealed class DockerHealthCheckFactory
    {
        private readonly WaitForDockerConfig config;

        public DockerHealthCheckFactory(WaitForDockerConfig config)
            => this.config = config;

        public DockerHealthChecker WithTcp(string serviceName, int port, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new TcpHealthChecker(port, timeoutInSeconds, serviceName, portOfDistinction, config.Logger);

        public DockerHealthChecker WithHttp(string serviceName, Uri url, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new HttpHealthCheck(serviceName, timeoutInSeconds, url, portOfDistinction, config.Logger);

        public DockerHealthChecker WithCmd(string serviceName, string command, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new DockerCommandHealthCheck(serviceName, config.DockerComposeProjectName, timeoutInSeconds, command, portOfDistinction, config.Logger);
    }
}