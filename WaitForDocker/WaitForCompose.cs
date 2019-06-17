using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaitForDocker.ComposeProcessing;
using WaitForDocker.Config;
using WaitForDocker.Logger;
using WaitForDocker.Shell;

namespace WaitForDocker
{
    internal class WaitForCompose
    {
        public static async Task Wait(Action<WaitForDockerComposeConfig> funcConfiguration = null)
        {
            var config = new WaitForDockerComposeConfig();
            funcConfiguration?.Invoke(config);

            var composeYaml = DockerFilesReader.ReadComposeContent(config.DockerComposeDirPath, config.ComposeFileName);
            var composeJson = new JsonComposeConverter().Convert(composeYaml);
            var ports = new JsonComposeServicesPortsExtractor().ExtractPorts(composeJson);

            var logger = config.Logger;
            logger.Log($"Checking is any port is already occupied before {DockerConsts.DockerCompose} execution..");

            var postComposeChecks = new List<Func<Task>>();
            var preComposeChecks = new List<Task>();
            foreach (var servicePort in ports)
            {
                preComposeChecks.Add(PreComposeCheck(logger, servicePort));
                postComposeChecks.Add(() => PostComposeCheck(servicePort, config, logger));
            }

            await Task.WhenAll(preComposeChecks);

            var composeCommand = ComposeBuilder.BuildComposeCommand(config);
            ShellConfiguratorFactory.GetShell(config.Logger).Execute(composeCommand, DockerConsts.DockerCompose);

            await Task.WhenAll(postComposeChecks.Select(check => check()));
        }

        public static Task Kill(Action<WaitForDockerComposeKillConfig> funcConfiguration = null)
        {
            var config = new WaitForDockerComposeKillConfig();
            funcConfiguration?.Invoke(config);
            var killCommand = ComposeBuilder.BuildComposeKillCommand(config);
            ShellConfiguratorFactory.GetShell(config.Logger).Execute(killCommand,DockerConsts.DockerComposeKill);
            return Task.CompletedTask;
        }

        private static async Task PreComposeCheck(ILogger logger, ServicePort servicePort)
        {
            var isServiceUp = await ServiceChecker.ServiceChecker.IsServiceUp(servicePort);
            logger.Log((isServiceUp ? "Warning! " : string.Empty) + $"service {servicePort.Name} on port: {servicePort.Port} was " + (isServiceUp ? string.Empty : "not ") + "occupied before docker compose execution");
        }

        private static async Task PostComposeCheck(ServicePort servicePort, WaitForDockerComposeConfig composeConfig, ILogger logger)
        {
            var isServiceUp = await ServiceChecker.ServiceChecker.IsServiceUp(servicePort, composeConfig.ServiceTimeoutInSeconds);
            if (!isServiceUp && composeConfig.ThrowOnServiceUnavailability)
                throw new WaitForDockerException($"Service: {servicePort.Name} on port {servicePort.Port} was unreachable after {composeConfig.ServiceTimeoutInSeconds} seconds.");

            var frontText = isServiceUp ? "Success!" : "Error!";
            logger.Log($"{frontText} service {servicePort.Name} on port {servicePort.Port} is " + (isServiceUp ? "ready" : "unreachable"));
        }
    }
}