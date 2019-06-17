using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using WaitForDocker.ComposeProcessing;

namespace WaitForDocker.ServiceChecker
{
    public static class ServiceChecker
    {
        private const string LocalHost = "127.0.0.1";

        public static async Task<bool> IsServiceUp(ServicePort servicePort, int? timeoutInSeconds = null)
        {
            return timeoutInSeconds.HasValue ? await CheckWithTimeout(servicePort, timeoutInSeconds.Value) : await IsAvailable(servicePort);
        }

        private static async Task<bool> CheckWithTimeout(ServicePort servicePort, int timeoutInSeconds)
        {
            var sp = new Stopwatch();
            sp.Start();
            while (sp.Elapsed.Seconds < timeoutInSeconds)
            {
                var isAvailable = await IsAvailable(servicePort);
                if (isAvailable)
                    return true;
            }
            return false;
        }

        private static async Task<bool> IsAvailable(ServicePort servicePort)
        {
            var client = new TcpClient();
            try
            {
                await client.ConnectAsync(LocalHost, servicePort.Port);

                if (client.Connected)
                    return client.Connected;

            }
            catch (Exception)
            {
                //ignore
            }
            return false;
        }
    }
}