using System.Threading.Tasks;
using WaitForDocker.Config;

namespace WaitForDocker
{
    public static class WaitFor
    {
        public static async Task DockerCompose(WaitForDockerConfig config = null)
           => await WaitForCompose.Wait(config);

        public static async Task DockerKill(WaitForDockerConfig config = null)
            => await WaitForDockerKill.Wait(config);
    }
}
