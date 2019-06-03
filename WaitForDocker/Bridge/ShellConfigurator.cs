using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WaitForDocker.Notification;

namespace WaitForDocker.Bridge
{
    public sealed class ShellConfigurator
    {
        private static IBridgeSystem BridgeSystem { get; set; }
        private static INotificationSystem NotificationSystem { get; set; }

        public ShellConfigurator(IBridgeSystem bridgeSystem, INotificationSystem notificationSystem = null)
        {
            BridgeSystem = bridgeSystem ?? throw new ArgumentException(nameof(bridgeSystem));

            NotificationSystem = notificationSystem ?? Notification.NotificationSystem.Default;

            if (!OS.IsWin())
            {
                Term("chmod +x cmd.sh");
            }
        }

        public static class OS
        {
            public static bool IsWin() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            public static bool IsMac() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

            public static bool IsGnu() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            public static string GetCurrent()
            {
                return
                    (IsWin() ? "win" : null) ??
                    (IsMac() ? "mac" : null) ??
                    (IsGnu() ? "gnu" : null);
            }
        }

        public Response Term(string command, Output? output = Output.Hidden, string dir = "")
        {
            var result = new Response();
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            var startInfo = new ProcessStartInfo
            {
                FileName = BridgeSystem.GetFileName(),
                Arguments = BridgeSystem.CommandConstructor(command, output, dir),
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
                        NotificationSystem.StandardLine();

                        while (!process.StandardOutput.EndOfStream)
                        {
                            var line = process.StandardOutput.ReadLine();
                            stdout.AppendLine(line);
                            NotificationSystem.StandardOutput(line);
                        }

                        while (!process.StandardError.EndOfStream)
                        {
                            var line = process.StandardError.ReadLine();
                            stderr.AppendLine(line);
                            NotificationSystem.StandardError(line);
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