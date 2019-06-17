using System.Threading.Tasks;
using WaitForDocker.Integrations.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace WaitForDocker.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper output;

        public Tests(ITestOutputHelper output)
        {
            this.output = output;
            WaitForDocker.Compose(config => { config.Logger = new XunitLogger(output); }).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task IntegrationTest()
        {
            await WaitForDocker.ComposeKill(config => { config.Logger = new XunitLogger(output); });
        }
    }
}
