using System;
using System.Collections.Concurrent;

namespace WaitForDocker.Logger
{
    public class DefaultLogger : ILogger
    {
        private readonly ConcurrentQueue<string> _logs = new ConcurrentQueue<string>();
        public string LogResult => string.Join(Environment.NewLine, _logs);

        public void Log(string message)
        {
            Console.WriteLine(message);
            _logs.Enqueue(message);
        }
    }
}