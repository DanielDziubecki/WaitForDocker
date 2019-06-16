using System;

namespace WaitForDocker
{
    public class WaitForDockerException : Exception
    {
        public WaitForDockerException(string message) : base(message)
        {
            
        }
    }
}