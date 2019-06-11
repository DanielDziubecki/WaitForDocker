using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WaitForDocker.Tests
{
    public class Tests
    {
        [Fact]
        public async Task Test()
        {
            await WaitForDocker.Compose(config => {
                config.ComposeFileName = "docker-compose-infrastructure.yaml";
            });
        }
    }
}
