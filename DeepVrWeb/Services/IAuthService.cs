using DeepVrLibrary.Models;
using DeepVrWeb.DTO;

namespace DeepVrWeb.Services;

public interface IAuthService
{
    public Task<AuthResponse> Login(AuthRequest authRequest);
    public Task<User?> Register(AuthRequest authRequest);
}