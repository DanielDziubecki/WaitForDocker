using System;
using WaitForDocker.Config;
using WaitForDocker.Logger;
using Xunit.Abstractions;

namespace WaitForDocker.Tests
{
    public class DockerFixture : IDisposable
    {
        private readonly ConsoleOutputLogger consoleOutputLogger;
        private readonly MessageSinkLogger messageSinkLogger;
        private readonly DefaultLogger defaultLogger;

        public DockerFixture(IMessageSink messageSink)
        {
            messageSinkLogger = new MessageSinkLogger(messageSink);
            consoleOutputLogger = new ConsoleOutputLogger();
            defaultLogger = new DefaultLogger();
            var config = new WaitForComposeConfigurationBuilder()
                .AddDockerServiceHealthCheck("rabbit", check => check.WithHttp(new Uri("sss")));
            WaitForDocker.Compose().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            WaitForDocker.ComposeKill().GetAwaiter().GetResult();
        }
    }
}