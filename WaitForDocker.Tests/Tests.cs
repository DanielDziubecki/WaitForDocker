using System.Threading.Tasks;
using WaitForDocker.Integrations.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace WaitForDocker.Tests
{
    [Collection(XunitConstants.DockerCollection)]
    public class Tests
    {

        [Fact]
        public async Task IntegrationTest()
        {
            Assert.True(true);
        }
    }
}
