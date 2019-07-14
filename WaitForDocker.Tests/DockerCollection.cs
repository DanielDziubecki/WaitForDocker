using Xunit;

namespace WaitForDocker.Tests
{
    [CollectionDefinition(XunitConstants.DockerCollection)]
    public class DockerCollection : ICollectionFixture<DockerFixture>
    {

    }
}