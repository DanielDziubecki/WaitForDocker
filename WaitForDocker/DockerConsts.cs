namespace WaitForDocker
{
    internal static class DockerConsts
    {
        public const string DockerCompose = "docker-compose";
        public const string DockerComposeKill = "docker-kill";
        public const string DefaultDockerComposeFile = "docker-compose.yaml";
        public const string DockerExec = "docker exec";
        public const string DockerComposeProjectNameParam = "-p ";
        public const string DockerComposeProjectName = "waitfordocker";
        public const int DockerServiceCheckTimeout = 10;
        public const int DockerComposeNumberOfInstances = 1;  // assuming that instance is always one, maybe it will change in future
    }
}
