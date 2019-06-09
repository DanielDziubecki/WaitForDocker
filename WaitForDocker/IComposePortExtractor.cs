using System.Collections.Generic;

namespace WaitForDocker
{
    public interface IComposePortExtractor<in T>
    {
        IEnumerable<ServicePort> ExtractPorts(T compose);
    }
}