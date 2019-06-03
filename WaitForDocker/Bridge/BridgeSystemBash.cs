using System;
using System.Diagnostics;
using System.IO;

namespace WaitForDocker.Bridge
{
    public static partial class BridgeSystem
    {
        public static IBridgeSystem Bash => new BridgeSystemBash();
    }

    public sealed class BridgeSystemBash : IBridgeSystem
    {
        public string GetFileName()
        {
            return "/bin/bash";
        }

        public string CommandConstructor(string command, Output? output = Output.Hidden, string dir = "")
        {
            if (!string.IsNullOrEmpty(dir))
            {
                dir = $" '{dir}'";
            }
            if (output == Output.External)
            {
                command = $"sh \"{Directory.GetCurrentDirectory()}/cmd.sh\" '{command}'{dir}";
            }
            command = $"-c \"{command}\"";
            return command;
        }

        public void Browse(string url)
        {
            Process.Start("open", url);
        }
    }
}
