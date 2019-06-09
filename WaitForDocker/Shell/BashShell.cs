using System.IO;

namespace WaitForDocker.Shell
{
    public static partial class ShellType
    {
        public static IShell Bash => new BashShell();
    }

    public sealed class BashShell : IShell
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
    }
}
