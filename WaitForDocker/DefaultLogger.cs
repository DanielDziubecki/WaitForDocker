using System;
using System.Collections.Concurrent;
using System.Linq;

namespace WaitForDocker
{
    public class DefaultLogger : ILogger
    {
        private readonly ConcurrentBag<string> logs = new ConcurrentBag<string>();
        public string LogResult => string.Join(Environment.NewLine, logs.Reverse());

        public void Log(string message) => logs.Add(message);

    }
}