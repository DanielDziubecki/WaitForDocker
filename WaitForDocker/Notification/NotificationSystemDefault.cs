using System;

namespace WaitForDocker.Notification
{
    public static class ShellOutputWriter
    {
        public static IShellOutputWriter Default => new DefaultShellOutputWriter();
    }

    public sealed class DefaultShellOutputWriter : IShellOutputWriter
    {
        public void StandardOutput(string message)
        {
            Console.WriteLine(message);
        }

        public void StandardError(string message)
        {
            Console.WriteLine(message);
        }

        public void StandardLine()
        {
            Console.WriteLine("");
        }
    }
}