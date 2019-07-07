using System;
using WaitForDocker.Shell;

namespace WaitForDocker.HealthCheckers
{
    public class HealthCheckFactory
    {
        public IServiceHealthChecker WithHttp(Uri url)
            => new HttpHealthCheck(url);

        public IServiceHealthChecker WithCmd(string command)
            => new CommandHealthCheck(command, ShellFactory.GetShell());
    }
}