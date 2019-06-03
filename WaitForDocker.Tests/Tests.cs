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
            await WaitForDocker.Compose(composeFileName: "docker-compose-infrastructure.yaml");
        }
    }
}
