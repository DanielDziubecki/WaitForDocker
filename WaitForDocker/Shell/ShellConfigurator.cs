using System;
using System.Diagnostics;
using System.Text;
using WaitForDocker.Notification;

namespace WaitForDocker.Shell
{
    public sealed class ShellConfigurator
    {
        private static IShell Shell { get; set; }
        private static IShellOutputWriter ShellOutputWriter { get; set; }

        public ShellConfigurator(IShell shell, IShellOutputWriter shellOutputWriter = null)
        {
            Shell = shell ?? throw new ArgumentException(nameof(shell));

            ShellOutputWriter = shellOutputWriter ?? Notification.ShellOutputWriter.Default;

            if (!OS.IsWin())
            {
                Term("chmod +x cmd.sh");
            }
        }

        public Response Term(string command, Output? output = Output.Hidden, string dir = "")
        {
            var result = new Response();
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            var startInfo = new ProcessStartInfo
            {
                FileName = Shell.GetFileName(),
                Arguments = Shell.CommandConstructor(command, output, dir),
                RedirectStandardInput = false,
                RedirectStandardOutput = (output != Output.External),
                RedirectStandardError = (output != Output.External),
                UseShellExecute = false,
                CreateNoWindow = (output != Output.External)
            };
            if (!string.IsNullOrEmpty(dir) && output != Output.External)
            {
                startInfo.WorkingDirectory = dir;
            }

            using (var process = Process.Start(startInfo))
            {
                switch (output)
                {
                    case Output.Internal:
                        ShellOutputWriter.StandardLine();

                        while (!process.StandardOutput.EndOfStream)
                        {
                            var line = process.StandardOutput.ReadLine();
                            stdout.AppendLine(line);
                            ShellOutputWriter.StandardOutput(line);
                        }

                        while (!process.StandardError.EndOfStream)
                        {
                            var line = process.StandardError.ReadLine();
                            stderr.AppendLine(line);
                            ShellOutputWriter.StandardError(line);
                        }
                        break;
                    case Output.Hidden:
                        stdout.AppendLine(process.StandardOutput.ReadToEnd());
                        stderr.AppendLine(process.StandardError.ReadToEnd());
                        break;
                }

                process.WaitForExit();
                result.Stdout = stdout.ToString();
                result.Stderr = stderr.ToString();
                result.Code = process.ExitCode;
            }

            return result;
        }
    }
}