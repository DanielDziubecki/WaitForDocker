using System.Collections.Generic;

namespace WaitForDocker
{
    public interface IComposeServicesPortsExtractor<in T>
    {
        IEnumerable<ServicePort> ExtractPorts(T compose);
    }
}