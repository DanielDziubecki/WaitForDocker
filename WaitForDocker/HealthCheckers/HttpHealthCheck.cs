using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WaitForDocker.HealthCheckers
{
    internal class HttpHealthCheck : IServiceHealthChecker
    {
        private readonly Uri url;
        private readonly HttpClient client;

        public HttpHealthCheck(Uri url)
        {
            this.url = url;
            client = new HttpClient();

        }
        public async Task<bool> IsHealthy()
        {
            var result = await client.GetAsync(url);
            return result.IsSuccessStatusCode;
        }
    }
}