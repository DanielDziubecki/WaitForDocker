using System;

namespace WaitForDocker
{
    internal class WaitForDockerException : Exception
    {
        public WaitForDockerException(string message) : base(message)
        {
            
        }
    }
}