using System;
using System.Threading.Tasks;
using WaitForDocker.Config;
using WaitForDocker.HealthCheckers;
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
                .AddHealthCheck(check => check.WithCustom(logger1 => new SomeHealthCheck("123", 1, null, logger1)))
                .Build();
            WaitFor.DockerCompose(config).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            WaitFor.DockerKill(config).GetAwaiter().GetResult();
        }
    }

    public class SomeHealthCheck : DockerHealthChecker
    {
        public SomeHealthCheck(string serviceName, int timeoutInSeconds, int? portOfDistinction, ILogger logger) :
            base(serviceName, logger, timeoutInSeconds, portOfDistinction)
        {
        }

        public override Task<bool> IsHealthy()
        {
            return Task.FromResult(true);
        }
    }
}