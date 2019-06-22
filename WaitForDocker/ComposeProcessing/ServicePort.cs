﻿namespace WaitForDocker.ComposeProcessing
{
    internal class ServicePort
    {
        public ServicePort(string name, int port)
        {
            Name = name;
            Port = port;
        }

        public string Name { get; }
        public int Port { get; }
    }
}