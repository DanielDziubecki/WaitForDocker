using System.Collections.Generic;
using System.Threading.Tasks;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;

namespace WaitForDocker.Tests
{
    [Collection(XunitConstants.DockerCollection)]
    public class Tests
    {
        private readonly ITestOutputHelper helper;

        public Tests(ITestOutputHelper helper)
        {
            this.helper = helper;
        }
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

        private async Task AddToRedis()
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            var db = redis.GetDatabase();
            await db.StringSetAsync("myKey", "123");
            helper.WriteLine("Redis key added");
        }

        private async Task PublishToRabbit()
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
            helper.WriteLine("Rabbitmq message published");
        }

        private async Task AddToMongoCollection()
        {
            var mongoClient = new MongoDB.Driver.MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("IntegrationTest");
            var col = db.GetCollection<TestClass>("TestClass");
            await col.InsertOneAsync(new TestClass { Test = 1 });
            helper.WriteLine("Mongodb document inserted");
        }

    }

    public class TestClass
    {
        public int Test { get; set; }
    }
}
