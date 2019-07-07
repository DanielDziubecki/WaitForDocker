using System.Collections.Generic;
using System.Threading.Tasks;
using WaitForDocker.Config;

namespace WaitForDocker
{
    public static class WaitForDocker
    {
        public static async Task Compose(WaitForDockerComposeConfig config = null)
             => await WaitForCompose.Wait(config);

        public static async Task ComposeKill(WaitForDockerComposeKillConfig config = null)
            => await WaitForCompose.Kill(config);
    }
}
