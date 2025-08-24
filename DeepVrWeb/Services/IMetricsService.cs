using DeepVrLibrary.DTO;
using DeepVrLibrary.Models;

namespace DeepVrWeb.Services;

public interface IMetricsService
{
    public Task SaveMetrics(MetricsMessage metricsMessage);
}