using System.Linq;
using System.Threading.Tasks;
using WaitForDocker.ComposeProcessing;
using WaitForDocker.Config;
using WaitForDocker.Shell;

namespace WaitForDocker
{
    internal class WaitForDockerKill
    {
        public static Task Wait(WaitForDockerConfig config)
        {
            config = config ?? new WaitForDockerConfig();
            var composeYaml = DockerFilesReader.ReadComposeContent(config.DockerComposeDirPath, config.ComposeFileName);
            var composeJson = new JsonComposeConverter().Convert(composeYaml);
            var servicePorts = new JsonComposeServicesPortsExtractor().ExtractServicePorts(composeJson).ToArray();

            var killCommand = DockerCommandBuilder.BuildDockerKillCommand(config,servicePorts);
            ShellExecutorFactory.GetShellExecutor(config.Logger).Execute(killCommand, DockerConsts.DockerComposeKill);
            return Task.CompletedTask;
        }
    }
}