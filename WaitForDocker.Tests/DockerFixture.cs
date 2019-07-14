using System;
using WaitForDocker.Config;
using WaitForDocker.Logger;
using Xunit.Abstractions;

namespace WaitForDocker.Tests
{
    public class DockerFixture : IDisposable
    {
        private WaitForDockerConfig config;

        public DockerFixture(IMessageSink messageSink)
        {
            var logger = new DefaultLogger();

            config = new WaitForDockerConfigurationBuilder()
                 .SetCustomLogger(logger)
                 .AddHealthCheck(check => check.WithHttp("rabbitmq", new Uri("http://localhost:15672"), portOfDistinction: 15672))
                 .AddHealthCheck(check => check.WithCmd("rabbitmq", "rabbitmqctl status", portOfDistinction: 5672))
                 .Build();
            WaitForDocker.Compose(config).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            WaitForDocker.Kill(config).GetAwaiter().GetResult();
        }
    }
}