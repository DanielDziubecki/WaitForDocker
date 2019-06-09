using System.IO;

namespace WaitForDocker
{
    public static class Helpers
    {
        public static string ReadComposeContent(string dockerComposeDirPath, string composeFileName)
        {
            var filePath = string.Empty;
            if (!string.IsNullOrWhiteSpace(dockerComposeDirPath))
            {
                filePath = Path.Combine(dockerComposeDirPath);
            }

            filePath = Path.Combine(filePath, !string.IsNullOrWhiteSpace(composeFileName) ? composeFileName : "docker-compose.yaml");

            var compose = File.ReadAllText(filePath);
            return compose;
        }
    }
}