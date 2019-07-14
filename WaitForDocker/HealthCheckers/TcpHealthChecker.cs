using System.Diagnostics;
using System.Threading.Tasks;
using WaitForDocker.Logger;

namespace WaitForDocker.HealthCheckers
{
    internal sealed class TcpHealthChecker : DockerHealthChecker
    {
        private readonly int servicePort;
       
        public TcpHealthChecker(int servicePort, int timeoutInSeconds,string serviceName, int? portOfDistinction, ILogger logger) : 
            base(serviceName, timeoutInSeconds, portOfDistinction, logger)
        {
            this.servicePort = servicePort;
        }

        public override async Task<bool> IsHealthy()
        {
            var sp = new Stopwatch();
            sp.Start();
            Logger.Log($"TCP health check of {ServiceName} on port {servicePort} has been started..");
            var attempts = 1;
            while (sp.Elapsed.Seconds < TimeoutInSeconds)
            {
                var isAvailable = await PortAvailabilityChecker.IsAvailable(servicePort);
                var result = isAvailable ? "successful" : "failed";
                Logger.Log($"{attempts} TCP health check of {ServiceName} on port {servicePort} was {result}");
                if (isAvailable)
                {
                    Logger.Log($"TCP health check of {ServiceName} returns success");
                    return true;
                }
                  
                attempts++;
            }

            var exceptionMessage = $"Health check failed! Service {ServiceName} was not available on port number {servicePort} after {TimeoutInSeconds} seconds.";
            Logger.Log(exceptionMessage);
            throw new WaitForDockerException(exceptionMessage);
        } 
    }
}