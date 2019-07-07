using WaitForDocker.Logger;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WaitForDocker.Tests
{
    public class MessageSinkLogger : ILogger
    {
        private readonly IMessageSink messageSink;

        public MessageSinkLogger(IMessageSink messageSink)
        {
            this.messageSink = messageSink;
        }
        public void Log(string message)
        {
            messageSink.OnMessage(new DiagnosticMessage(message));
        }
    }
}