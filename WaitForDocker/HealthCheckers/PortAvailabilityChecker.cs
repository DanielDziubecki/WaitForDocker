using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WaitForDocker.HealthCheckers
{
    internal static class PortAvailabilityChecker
    {
        private const string LocalHost = "127.0.0.1";

        public static async Task<bool> IsAvailable(int servicePort)
        {
            var client = new TcpClient();
            try
            {
                await client.ConnectAsync(LocalHost, servicePort);

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