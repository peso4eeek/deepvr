using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeepVrLibrary;
using DeepVrLibrary.Exceptions;
using DeepVrLibrary.Models;
using DeepVrWeb.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DeepVrWeb.Services;

public class AuthService(MyDbContext context, IConfiguration configuration) : IAuthService
{
    public async Task<AuthResponse> Login(AuthRequest authRequest)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Name.Equals(authRequest.Name));

        if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user.Password))
        {
            throw new AuthenticationException("Неверное имя пользователя или пароль");
        }

        var accessToken = this.GenerateAccessToken(user);
        var refreshToken = this.GenerateRefreshToken();

        return new AuthResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

    }


    public async Task<User?> Register(AuthRequest authRequest)
    {
        if (await context.Users.AnyAsync(u => u.Name.Equals(authRequest.Name)))
        {
            throw new AuthenticationException("Пользователь с такими данными уже существует");
        }

        var user = new User()
        {
            Name = authRequest.Name,
            Password = BCrypt.Net.BCrypt.HashPassword(authRequest.Password),
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }
    private string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));

        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings: Issuer"],
            audience: configuration["JwtSettings: Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JwtSettings: Expiration"])),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}