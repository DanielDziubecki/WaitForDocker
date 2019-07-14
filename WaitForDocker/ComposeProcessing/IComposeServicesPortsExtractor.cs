using System.Collections.Generic;

namespace WaitForDocker.ComposeProcessing
{
    internal interface IComposeServicesPortsExtractor<in T>
    {
        IEnumerable<ServicePort> ExtractServicePorts(T compose);
    }
}