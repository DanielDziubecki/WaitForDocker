using System;
using WaitForDocker.Integrations.Xunit;
using Xunit.Abstractions;

namespace WaitForDocker.Tests
{
    public class DockerFixture : IDisposable
    {
        private readonly XunitMessageSinkLogger xunitLogger;

        public DockerFixture(IMessageSink output)
        {
            xunitLogger = new XunitMessageSinkLogger(output);
            WaitForDocker.Compose(config => config.Logger = xunitLogger).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            WaitForDocker.ComposeKill(config => config.Logger = xunitLogger).GetAwaiter().GetResult();
        }
    }
}