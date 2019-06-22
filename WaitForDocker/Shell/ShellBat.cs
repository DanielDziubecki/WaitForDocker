namespace WaitForDocker.Shell
{
    internal static partial class ShellType
    {
        public static IShell Bat => new ShellBat();
    }

    internal sealed class ShellBat : IShell
    {
        public string GetFileName()
        {
            return "cmd.exe";
        }

        public string CommandConstructor(string command)
        {
            command = $"/c \"{command}\"";
            return command;
        }
    }
}
