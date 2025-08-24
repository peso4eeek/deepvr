namespace DeepVrLibrary.DTO;

public class MetricsMessage
{
    public required string Uuid { get; set; }
    public required float Cpu {get; set;}
    public required float Ram {get; set;}
    public required DateTime ReceivedAt { get; set; }
}