using System.Threading.Tasks;

namespace WaitForDocker.HealthCheckers
{
    public interface IServiceHealthChecker
    {
        Task<bool> IsHealthy();
    }
}