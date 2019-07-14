using System;
using System.Collections.Concurrent;

namespace WaitForDocker.Logger
{
    public class DefaultLogger : ILogger
    {
        private readonly ConcurrentQueue<string> logs = new ConcurrentQueue<string>();
        public string LogResult => string.Join(Environment.NewLine, logs);

        public void Log(string message)
        {
            Console.WriteLine(message);
            logs.Enqueue(message);
        }
    }
}