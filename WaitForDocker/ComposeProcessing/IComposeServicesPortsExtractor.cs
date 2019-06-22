using System.Collections.Generic;

namespace WaitForDocker.ComposeProcessing
{
    internal interface IComposeServicesPortsExtractor<in T>
    {
        IEnumerable<ServicePort> ExtractPorts(T compose);
    }
}