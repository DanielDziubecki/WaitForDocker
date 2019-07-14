using System.Linq;
using System.Threading.Tasks;
using WaitForDocker.ComposeProcessing;
using WaitForDocker.Config;
using WaitForDocker.HealthCheckers;
using WaitForDocker.Shell;

namespace WaitForDocker
{
    internal class WaitForCompose
    {
        public static async Task Wait(WaitForDockerConfig config)
        {
            config = config ?? new WaitForDockerConfig();
            var logger = config.Logger;
            logger.Log("Wait for docker has been started..");
            var composeYaml = DockerFilesReader.ReadComposeContent(config.DockerComposeDirPath, config.ComposeFileName);
            var composeJson = new JsonComposeConverter().Convert(composeYaml);
            var servicePorts = new JsonComposeServicesPortsExtractor().ExtractServicePorts(composeJson).ToArray();

            logger.Log($"Checking is any port is already occupied before {DockerConsts.DockerCompose} execution..");
            await DockerHealthCheckRunner.RunPreComposeHealthChecks(servicePorts, config.Logger);

            config.ExtendWithDefaultHealthChecks(servicePorts);
            var composeCommand = DockerCommandBuilder.BuildComposeCommand(config);
            ShellExecutorFactory.GetShellExecutor(config.Logger).Execute(composeCommand, DockerConsts.DockerCompose);

            await DockerHealthCheckRunner.RunPostComposeHealthChecks(config.HealthCheckers);
            logger.Log("All health checks returns success.");
            logger.Log("Wait for docker has been finished..");
        }
    }
}