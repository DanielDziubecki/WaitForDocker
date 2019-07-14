using System;
using WaitForDocker.Config;
using WaitForDocker.Logger;

namespace WaitForDocker.HealthCheckers
{
    public sealed class DockerHealthCheckFactory
    {
        private readonly WaitForDockerConfig config;

        public DockerHealthCheckFactory(WaitForDockerConfig config)
            => this.config = config;

        public DockerHealthChecker WithTcp(string serviceName, int port, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new TcpHealthChecker(serviceName,port,config.Logger,timeoutInSeconds,portOfDistinction);

        public DockerHealthChecker WithHttp(string serviceName, Uri url, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new HttpHealthCheck(serviceName,url,config.Logger,timeoutInSeconds,portOfDistinction);

        public DockerHealthChecker WithCmd(string serviceName, string command, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new DockerCommandHealthCheck(serviceName, config.DockerComposeProjectName, timeoutInSeconds, command, portOfDistinction, config.Logger);

        public DockerHealthChecker WithCustom(Func<ILogger, DockerHealthChecker> func)
            => func(config.Logger);
    }
}