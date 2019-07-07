using System.Collections.Generic;
using System.Threading.Tasks;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using StackExchange.Redis;
using Xunit;

namespace WaitForDocker.Tests
{
    [Collection(XunitConstants.DockerCollection)]
    public class Tests
    {
        [Fact]
        public async Task IntegrationTest()
        {
            var tasks = new Task[]
            {
                AddToMongoCollection(),
                PublishToRabbit(),
                AddToRedis()
            };
            await Task.WhenAll(tasks);
        }

        private static async Task AddToRedis()
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            var db = redis.GetDatabase();
            await db.StringSetAsync("myKey", "123");
        }

        private static async Task PublishToRabbit()
        {
            var config = new RawRabbitConfiguration
            {
                Username = "guest",
                Password = "guest",
                Port = 5672,
                VirtualHost = "/",
                Hostnames = new List<string> { "localhost" }
            };
            var client = BusClientFactory.CreateDefault(config);
            await client.PublishAsync(new TestClass { Test = 1 });
        }

        private static async Task AddToMongoCollection()
        {
            var mongoClient = new MongoDB.Driver.MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("IntegrationTest");
            var col = db.GetCollection<TestClass>("TestClass");
            await col.InsertOneAsync(new TestClass { Test = 1 });
        }

    }

    public class TestClass
    {
        public int Test { get; set; }
    }
}
