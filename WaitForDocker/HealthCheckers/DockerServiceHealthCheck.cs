namespace WaitForDocker.HealthCheckers
{
    public class DockerServiceHealthCheck
    {
        public string ServiceName { get; }
        public IServiceHealthChecker HealthChecker { get; }

        public DockerServiceHealthCheck(string serviceName, IServiceHealthChecker healthChecker)
        {
            ServiceName = serviceName;
            HealthChecker = healthChecker;
        }
    }
}