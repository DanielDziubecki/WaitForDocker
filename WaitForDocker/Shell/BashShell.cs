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

        public string CommandConstructor(string command)
        {
            command = $"-c \"{command}\"";
            return command;
        }
    }
}
