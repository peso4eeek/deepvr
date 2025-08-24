using DeepVrLibrary.DTO;
using DeepVrWeb.Services;
using Microsoft.AspNetCore.SignalR;

namespace DeepVrWeb.Controllers;

public class MetricsHub(IMetricsService metricsService): Hub
{ 
    public async Task SendMetrics(MetricsMessage metrics)
    {
        try
        {
            await metricsService.SaveMetrics(metrics);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
}