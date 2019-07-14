using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaitForDocker.ComposeProcessing;
using WaitForDocker.Logger;

namespace WaitForDocker.HealthCheckers
{
    internal static class DockerHealthCheckRunner
    {
        public static Task RunPostComposeHealthChecks(IEnumerable<DockerHealthChecker> healthCheckers)
            => Task.WhenAll(healthCheckers.Select(x => x.IsHealthy()));

        public static Task RunPreComposeHealthChecks(IEnumerable<ServicePort> servicePorts, ILogger logger)
            => Task.WhenAll(servicePorts.Select(x => GetPreComposeCheck(x, logger)));

        private static async Task GetPreComposeCheck(ServicePort servicePort, ILogger logger)
        {
            var isServiceUp = await PortAvailabilityChecker.IsAvailable(servicePort.Port);
            logger.Log((isServiceUp ? "Warning! " : string.Empty) +
                       $"service {servicePort.Name} on port: {servicePort.Port} was " +
                       (isServiceUp ? string.Empty : "not ") + "occupied before docker compose execution");
        }
    }
}
