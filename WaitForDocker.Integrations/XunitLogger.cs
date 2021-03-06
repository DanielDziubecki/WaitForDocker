﻿using WaitForDocker.Logger;
using Xunit.Abstractions;

namespace WaitForDockers.Integrations.Xunit
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
}
