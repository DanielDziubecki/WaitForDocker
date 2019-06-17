using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace WaitForDocker.ComposeProcessing
{
    public class JsonComposeServicesPortsExtractor : IComposeServicesPortsExtractor<string>
    {
        private const string ComposeServices = "services";
        private const string ComposePorts = "ports";

        public IEnumerable<ServicePort> ExtractPorts(string jsonCompose)
        {
            var servicesRoot = JObject.Parse(jsonCompose)[ComposeServices];

            var servicePorts = new List<ServicePort>();
            foreach (var service in servicesRoot.Children().Where(v => v.HasValues).Select(o => o.Children()))
            {
                var portsRoot = service[ComposePorts];

                if (!portsRoot.Any()) continue;

                foreach (var port in portsRoot.Children())
                {
                    var portValue = port.Value<string>();
                    if (!portValue.Contains(":"))
                    {
                        continue;
                    }

                    var splittedPorts = portValue.Split(':');
                    var serviceName = port.Parent.Path.Split('.')[1];
                    servicePorts.Add(new ServicePort (serviceName, int.Parse(splittedPorts[0])));
                }
            }
            return servicePorts;
        }
    }
}