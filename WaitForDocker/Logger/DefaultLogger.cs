using System;
using System.Collections.Concurrent;
using System.Linq;

namespace WaitForDocker.Logger
{
    public class DefaultLogger : ILogger
    {
        private readonly ConcurrentQueue<string> logs = new ConcurrentQueue<string>();
        public string LogResult => string.Join(Environment.NewLine, logs);

        public void Log(string message) => logs.Enqueue(message);

    }
}