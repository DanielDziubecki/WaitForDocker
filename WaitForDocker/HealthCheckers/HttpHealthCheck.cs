using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using WaitForDocker.Logger;

namespace WaitForDocker.HealthCheckers
{
    internal sealed class HttpHealthCheck : DockerHealthChecker
    {
        private readonly Uri url;
        private readonly HttpClient client;

        public HttpHealthCheck(string serviceName, int timeoutInSeconds, Uri url, int? portOfDistinction, ILogger logger) :
            base(serviceName, timeoutInSeconds, portOfDistinction, logger)
        {
            this.url = url;
            client = new HttpClient();

        }
        public override async Task<bool> IsHealthy()
        {
            Logger.Log($"HTTP health check of {ServiceName} on url {url} has been started..");
            var sp = new Stopwatch();
            var attempts = 0;
            while (sp.Elapsed.Seconds < TimeoutInSeconds)
            {
                try
                {
                    sp.Start();
                    attempts++;
                    var result = await client.GetAsync(url);
                    Logger.Log($"Attempt number {attempts} of {ServiceName} HTTP health check returns {result.StatusCode} status code.");
                    if (result.IsSuccessStatusCode)
                    {
                        Logger.Log($"HTTP health check of service {ServiceName} returns success status code");
                        return true;
                    }
                        

                    sp.Stop();
                    await Task.Delay(300);

                }
                catch (Exception e)
                {
                    sp.Stop();
                    //ignore
                    Logger.Log($"Attempt number {attempts} of {ServiceName} HTTP health check returns exception with following message: {e.Message}");
                    await Task.Delay(300);
                }
            }

            var exceptionMessage = $"Health check failed! Service {ServiceName} was not returning success http status code after {TimeoutInSeconds} seconds.";
            Logger.Log(exceptionMessage);
            throw new WaitForDockerException(exceptionMessage);
        }
    }
}