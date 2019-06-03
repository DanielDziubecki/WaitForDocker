using System;
using System.Diagnostics;
using System.IO;

namespace WaitForDocker.Bridge
{
    public static partial class BridgeSystem
    {
        public static IBridgeSystem Bat => new BridgeSystemBat();
    }

    public sealed class BridgeSystemBat : IBridgeSystem
    {
        public string GetFileName()
        {
            return "cmd.exe";
        }

        public string CommandConstructor(string command, Output? output = Output.Hidden, string dir = "")
        {
            if (!string.IsNullOrEmpty(dir))
            {
                dir = $" \"{dir}\"";
            }
            if (output == Output.External)
            {
                command = $"\"{Directory.GetCurrentDirectory()}/cmd.bat\" \"{command}\"{dir}";
            }
            command = $"/c \"{command}\"";
            return command;
        }

        public void Browse(string url)
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }
}
