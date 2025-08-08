namespace DeepVrLibrary.Models;

public class User
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Password { get; set; }

    public string? RefreshToken { get; set; }
    
    public UserRole Role { get; set; }
    
}