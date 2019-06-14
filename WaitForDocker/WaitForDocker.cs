using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitForDocker
{
    public class WaitForDocker
    {
        public static Task Container(Action<WaitForDockerConfig> funcConfiguration = null)
        {
            return Task.CompletedTask;
        }

        public static async Task Compose(Action<WaitForDockerConfig> funcConfiguration = null)
        {
            var config = new WaitForDockerConfig();
            funcConfiguration?.Invoke(config);

            var composeYaml = Helpers.ReadComposeContent(config.DockerComposeDirPath, config.ComposeFileName);
            var composeJson = new JsonComposeConverter().Convert(composeYaml);
            var ports = new JsonComposeServicesPortsExtractor().ExtractPorts(composeJson);

            var logger = config.Logger;
            logger.Log("Checking is any port is already occupied before docker-compose execution..");

            var futureChecks = new List<Func<Task<bool>>>();
            foreach (var servicePort in ports)
            {
                Task<bool> ServiceCheck() => ServiceChecker.IsServiceUp(servicePort.Port);
                futureChecks.Add(ServiceCheck);
                logger.Log($"Service: {servicePort.Name} Port: {servicePort.Port} Occupied: {await ServiceCheck()}");
            }

            var command = ComposeBuilder.BuildComposeCommand(config);
            var shell = ShellConfiguratorFactory.GetShell(config.Logger);
            shell.Execute(command);

            await Task.WhenAll(futureChecks.Select(check => check()));
        }
    }

    public class DefaultLogger : ILogger
    {
        private readonly StringBuilder _builder = new StringBuilder();
        public string LogResult => _builder.ToString();

        public void Log(string message) => _builder.AppendLine(message);

    }

    public interface ILogger
    {
        void Log(string message);
    }
}
