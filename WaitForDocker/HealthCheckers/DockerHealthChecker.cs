﻿using System.Threading.Tasks;
using WaitForDocker.Logger;

namespace WaitForDocker.HealthCheckers
{
    public abstract class DockerHealthChecker
    {
        public string ServiceName { get; }
        protected int TimeoutInSeconds { get; }
        public int? PortOfDistinction { get; }
        protected ILogger Logger { get; }


        protected DockerHealthChecker(string serviceName, int timeoutInSeconds, int? portOfDistinction, ILogger logger)
        {
            ServiceName = serviceName;
            Logger = logger;
            TimeoutInSeconds = timeoutInSeconds;
            PortOfDistinction = portOfDistinction;
        }

        public abstract Task<bool> IsHealthy();
    }
}