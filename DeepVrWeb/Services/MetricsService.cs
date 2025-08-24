using DeepVrLibrary;
using DeepVrLibrary.DTO;
using DeepVrLibrary.Exceptions;
using DeepVrLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepVrWeb.Services;

public class MetricsService(MyDbContext dbContext): IMetricsService
{

    public async Task SaveMetrics(MetricsMessage metricsMessage)
    {
        var pc = await dbContext.Pcs.SingleOrDefaultAsync(p => p.Uuid == metricsMessage.Uuid);

        if (pc == null)
        {
            throw new NotFoundException("the pc that sent the metrics was not found");
        }
        var metrics = new Metrics()
        {
            Uuid = metricsMessage.Uuid,
            Ip = pc.Ip,
            Ram = metricsMessage.Ram,
            Cpu = metricsMessage.Cpu,
            ReceivedAt = metricsMessage.ReceivedAt,
        };
        await dbContext.AddAsync(metrics);
        
        await dbContext.SaveChangesAsync();
        
    }
}