namespace DeepVrLibrary.Models;

public class Pc
{
    public int Id { get; set; }
    public string Uuid { get; set; }
    public string Ip {get; set;}
    public string HostName { get; set; }
    public bool IsBusy { get; set; }
}