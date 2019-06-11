using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WaitForDocker
{
    public static class ServiceAvailabilityChecker
    {
        private const string LocalHost = "127.0.0.1";

        public static async Task<bool> IsServiceAvailable(int port, int timeoutInSeconds)
        {
            var client = new TcpClient();
            var sp = new Stopwatch();
            sp.Start();
            while (sp.Elapsed.Seconds < timeoutInSeconds)
            {
                try
                {
                    await client.ConnectAsync(LocalHost, port);

                    if (client.Connected)
                        return client.Connected;
                }
                catch (Exception e)
                {
                    //ignore
                }
            }
            return false;
        }
    }
}