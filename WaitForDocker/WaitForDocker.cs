using System;
using System.Threading.Tasks;
using WaitForDocker.Config;

namespace WaitForDocker
{
    public class WaitForDocker
    {
        public static async Task Compose(Action<WaitForDockerComposeConfig> funcConfiguration = null)
             => await WaitForCompose.Wait(funcConfiguration);

        public static async Task ComposeKill(Action<WaitForDockerComposeKillConfig> funcConfiguration = null)
            => await WaitForCompose.Kill(funcConfiguration);
    }
}
