using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using WaitForDocker.Notification;

namespace WaitForDocker
{
    public class WaitForDocker
    {
        public static Task Container(string dockerFileDirPath, int serviceTimeoutInSeconds = 60, IShellOutputWriter shellOutputWriter = null)
        {
            return Task.CompletedTask;
        }

        public static Task Compose(Action<WaitForDockerConfig> funcConfiguration = null)
        {
            var config = new WaitForDockerConfig();
            funcConfiguration?.Invoke(config);

            var composeYaml = Helpers.ReadComposeContent(config.DockerComposeDirPath, config.ComposeFileName);
            var composeJson = new JsonComposeConverter().Convert(composeYaml);
            var ports = new JsonComposeServicesPortsExtractor().ExtractPorts(composeJson);



            var command = ComposeBuilder.BuildComposeCommand(config);
            var shell = ShellConfiguratorFactory.GetShell(config.ShellOutputWriter);
            var result = shell.Term(command);

            return Task.CompletedTask;
        }
    }

    public static class ServiceChecker
    {
        private const string LocalHost = "127.0.0.1";

        public static async Task<bool> IsServiceUp(int port, int timeoutInSeconds)
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
