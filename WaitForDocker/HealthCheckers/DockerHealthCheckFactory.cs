using System;
using WaitForDocker.Config;
using WaitForDocker.Logger;

namespace WaitForDocker.HealthCheckers
{
    public sealed class DockerHealthCheckFactory
    {
        private readonly WaitForDockerConfig _config;

        public DockerHealthCheckFactory(WaitForDockerConfig config)
            => _config = config;

        public DockerHealthChecker WithTcp(string serviceName, int port, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new TcpHealthChecker(serviceName,port,_config.Logger,timeoutInSeconds,portOfDistinction);

        public DockerHealthChecker WithHttp(string serviceName, Uri url, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new HttpHealthCheck(serviceName,url,_config.Logger,timeoutInSeconds,portOfDistinction);

        public DockerHealthChecker WithCmd(string serviceName, string command, int timeoutInSeconds = DockerConsts.DockerServiceCheckTimeout, int? portOfDistinction = null)
            => new DockerCommandHealthCheck(serviceName, command, timeoutInSeconds, _config.DockerComposeProjectName, portOfDistinction, _config.Logger);

        public DockerHealthChecker WithCustom(Func<ILogger, DockerHealthChecker> func)
            => func(_config.Logger);
    }
}