using System.IO;

namespace WaitForDocker
{
    internal static class DockerFilesReader
    {
        public static string ReadComposeContent(string dockerComposeDirPath, string composeFileName)
        {
            var filePath = string.Empty;
            if (!string.IsNullOrWhiteSpace(dockerComposeDirPath))
            {
                filePath = Path.Combine(dockerComposeDirPath);
            }

            filePath = Path.Combine(filePath, !string.IsNullOrWhiteSpace(composeFileName) ? composeFileName : DockerConsts.DefaultDockerComposeFile);

            var compose = File.ReadAllText(filePath);
            return compose;
        }
    }
}