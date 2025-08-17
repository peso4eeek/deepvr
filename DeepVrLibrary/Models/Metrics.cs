namespace DeepVrLibrary.Models;

public class Metrics
{
    public int Id { get; set; }
    public required string Uuid { get; set; }
    public required string Ip  { get; set; }
    public required string Cpu {get; set;}
    public required string Ram { get; set; }
    public required DateTime ReceivedAt { get; set; }
}