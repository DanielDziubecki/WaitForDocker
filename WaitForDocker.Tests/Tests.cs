using System.Threading.Tasks;
using WaitForDocker.Integrations.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace WaitForDocker.Tests
{
    [Collection(XunitConstants.DockerCollection)]
    public class Tests
    {
        private readonly ITestOutputHelper output;

        public Tests(ITestOutputHelper output)
        {
          // this.output = output;
          // ILogger logger = new DefaultLogger();
          // WaitForDocker.Compose(config => { config.Logger = logger; }).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task IntegrationTest()
        {
            Assert.True(true);
           // await WaitForDocker.ComposeKill(config => { config.Logger = new XunitLogger(output); });
        }
    }
}
