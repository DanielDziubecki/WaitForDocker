using System;
using System.IO;
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

        public static Task Compose(Func<WaitForDockerConfig, WaitForDockerConfig> funcConfiguration = null)
        {
            var config = new WaitForDockerConfig();
            config = funcConfiguration == null ? config : funcConfiguration(config);

            var composeYaml = Helpers.ReadComposeContent(config.DockerComposeDirPath, config.ComposeFileName);
            var composeJson = new JsonComposeConverter().Convert(composeYaml);
            var ports = new JsonComposePortExtractor().ExtractPorts(composeJson);

            var command = ComposeBuilder.BuildComposeCommand(config.DockerComposeDirPath);
            var shell = ShellConfiguratorFactory.GetShell(config.ShellOutputWriter);
            var result = shell.Term(command);

            return Task.CompletedTask;
        }
    }
}
