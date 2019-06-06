using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WaitForDocker.Bridge;
using WaitForDocker.Notification;
using YamlDotNet.Serialization;

namespace WaitForDocker
{
    public class WaitForDocker
    {


        public static Task Container(string dockerFileDirPath, int serviceTimeoutInSeconds = 60, INotificationSystem notificationSystem = null)
        {
            return Task.CompletedTask;
        }

        public static Task Compose(string dockerComposeDirPath = null, string composeFileName = null, int serviceTimeoutInSeconds = 60, INotificationSystem notificationSystem = null)
        {
            var command = BuildComposeCommand(dockerComposeDirPath, composeFileName);
            GetServicesPorts(dockerComposeDirPath, composeFileName);
            //var shell = GetShell();
            //var result = shell.Term(command);
            return Task.CompletedTask;
        }

        private static void GetServicesPorts(string dockerComposeDirPath, string composeFileName)
        {
            var filePath = string.Empty;
            if (!string.IsNullOrWhiteSpace(dockerComposeDirPath))
            {
                filePath = Path.Combine(dockerComposeDirPath);
            }

            filePath = Path.Combine(filePath, !string.IsNullOrWhiteSpace(composeFileName) ?
                composeFileName : "docker-compose.yaml");

            var compose = File.ReadAllText(filePath);
            var deserializer = new Deserializer();
            var yamlObject = deserializer.Deserialize<dynamic>(compose);

            var x = JsonConvert.SerializeObject(yamlObject);
            JObject jo = JObject.Parse(x)["services"];
           var e = jo.Children().ToList();

           foreach (var child in jo.Children().Where(v=>v.HasValues).Select(o=>o.Children()))
           {
               var ports = child["ports"];

               if (!ports.Any()) continue;
               foreach (var jToken in ports.Children())
               {
                   var tokenValue = jToken.Value<string>();
                   if (!tokenValue.Contains(":"))
                   {
                       throw new Exception("Port not exposed");
                   }

                   var splittedPorts = tokenValue.Split(':');
                   var client = new TcpClient("localhost", int.Parse(splittedPorts[0]));
                 //  client.c;
               }
           }
        }

        public class ServicePort
        {
            public string Name { get; set; }
            public int Port { get; set; }
        }

        private static ShellConfigurator GetShell()
        {
            IBridgeSystem bridgeSystem;
            switch (ShellConfigurator.OS.GetCurrent())
            {
                case "win":
                    bridgeSystem = BridgeSystem.Bat;
                    break;
                case "mac":
                case "gnu":
                    bridgeSystem = BridgeSystem.Bash;
                    break;

                default:
                    throw new NotSupportedException();
            }

            return new ShellConfigurator(bridgeSystem, NotificationSystem.Default);
        }

        private static string BuildComposeCommand(string dockerComposeDirPath, string composeFileName)
        {
            var cmd = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(dockerComposeDirPath))
            {
                cmd.Append($@"cd {dockerComposeDirPath} && ");
            }


            cmd.Append(string.IsNullOrWhiteSpace(composeFileName)
                ? "docker-compose up"
                : $@"docker-compose -f .\{composeFileName} up");

            return cmd.ToString();
        }


      

    }

    public class DockerCompose
    {
        public string version { get; set; }
        public Service services { get; set; }
    }

    public class Service
    {
        public string[] ports { get; set; }
    }
}
