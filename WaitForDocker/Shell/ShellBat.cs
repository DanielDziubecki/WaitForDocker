using System.IO;

namespace WaitForDocker.Shell
{
    public static partial class ShellType
    {
        public static IShell Bat => new ShellBat();
    }

    public sealed class ShellBat : IShell
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
    }
}
