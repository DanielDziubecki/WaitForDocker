namespace WaitForDocker.Shell
{
    internal static partial class ShellType
    {
        public static IShell Bash => new BashShell();
    }

    internal sealed class BashShell : IShell
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
