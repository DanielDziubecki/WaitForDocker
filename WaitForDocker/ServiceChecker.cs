using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WaitForDocker
{
    public static class ServiceChecker
    {
        private const string LocalHost = "127.0.0.1";

        public static async Task<bool> IsServiceUp(int port, int? timeoutInSeconds = null)
        {
            return timeoutInSeconds.HasValue ? await CheckWithTimeout(port, timeoutInSeconds.Value) : await IsAvailable(port);
        }

        private static async Task<bool> CheckWithTimeout(int port, int timeoutInSeconds)
        {
            var sp = new Stopwatch();
            sp.Start();
            while (sp.Elapsed.Seconds < timeoutInSeconds)
            {
                var isAvailable = await IsAvailable(port);
                if (isAvailable)
                    return true;
            }
            return false;
        }

        private static async Task<bool> IsAvailable(int port)
        {
            var client = new TcpClient();
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
            return false;
        }
    }
}