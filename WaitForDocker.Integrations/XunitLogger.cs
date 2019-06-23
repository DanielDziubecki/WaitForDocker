using System;
using System.Diagnostics;
using WaitForDocker.Logger;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WaitForDocker.Integrations.Xunit
{
    public class XunitLogger : ILogger
    {
        private readonly ITestOutputHelper output;

        public XunitLogger(ITestOutputHelper output)
        {
            this.output = output;
        }
        public void Log(string message)
        {
            output.WriteLine(message);
        }
    }

    public class XunitMessageSinkLogger : ILogger
    {
        private readonly IMessageSink messageSink;

        public XunitMessageSinkLogger(IMessageSink messageSink)
        {
            this.messageSink = messageSink;
        }

        public void Log(string message)
        {
            Debug.WriteLine(message);
            messageSink.OnMessage(new DiagnosticMessage(message));
        }
    }
}
