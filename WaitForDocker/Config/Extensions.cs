using System.Collections.Generic;
using System.Linq;
using WaitForDocker.ComposeProcessing;
using WaitForDocker.HealthCheckers;

namespace WaitForDocker.Config
{
    internal static class Extensions
    {
        internal static void ExtendWithDefaultHealthChecks(this WaitForDockerConfig config,
            IEnumerable<ServicePort> servicePorts)
        {
            var enumerable = config.HealthCheckers.Select(e => new { e.ServiceName, e.PortOfDistinction });
            var defaltHealthChecksPorts = servicePorts.Where(x => !enumerable.Any(e => e.PortOfDistinction == x.Port && e.ServiceName == x.Name));
            config.HealthCheckers.AddRange(defaltHealthChecksPorts.Select(x => new TcpHealthChecker(x.Name,x.Port,config.Logger,DockerConsts.DockerServiceCheckTimeout,null)));
        }
    }
}