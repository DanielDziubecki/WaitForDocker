using System.Collections.Generic;

namespace WaitForDocker.ComposeProcessing
{
    public interface IComposeServicesPortsExtractor<in T>
    {
        IEnumerable<ServicePort> ExtractPorts(T compose);
    }
}