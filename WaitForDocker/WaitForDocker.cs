using System.Threading.Tasks;
using WaitForDocker.Config;

namespace WaitForDocker
{
    public static class WaitForDocker
    {
        public static async Task Compose(WaitForDockerConfig config = null)
           => await WaitForCompose.Wait(config);


        public static async Task Kill(WaitForDockerConfig config = null)
            => await WaitForDockerKill.Wait(config);
    }
}
