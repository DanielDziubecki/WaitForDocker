using System;

namespace WaitForDocker.Tests
{
    public class DockerFixture : IDisposable
    {
        private readonly ConsoleOutputLogger consoleOutputLogger;

        public DockerFixture()
        {
            consoleOutputLogger = new ConsoleOutputLogger();
            WaitForDocker.Compose(config => config.Logger = consoleOutputLogger).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            WaitForDocker.ComposeKill(config => config.Logger = consoleOutputLogger).GetAwaiter().GetResult();
        }
    }
}