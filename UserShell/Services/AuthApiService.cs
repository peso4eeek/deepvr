using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DeepVrWeb.DTO;

namespace UserShell.Services;

public class AuthApiService
{
	private readonly HttpClient _httpClient;

	public AuthApiService()
	{
		_httpClient = new HttpClient
		{
			BaseAddress = new System.Uri("http://localhost:5095")
		};
	}
	public async Task<AuthResponse> LoginAsync(string userName, string password)
	{
		var request = new AuthRequest()
		{
			Name = userName,
			Password = password
		};
		var response = await _httpClient.PostAsJsonAsync("/auth/login", request);
		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			throw new System.Exception(string.IsNullOrWhiteSpace(error) ? $"Ошибка входа: {(int)response.StatusCode}" : error);
		}
		var data = await response.Content.ReadFromJsonAsync<AuthResponse>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
		return data ?? new AuthResponse();
	}

	public async Task RegisterAsync(string userName, string password)
	{
		var request = new AuthRequest()
		{
			Name = userName,
			Password = password
		};
		var response = await _httpClient.PostAsJsonAsync("/auth/register", request);
		if (!response.IsSuccessStatusCode)
		{
			var error = await response.Content.ReadAsStringAsync();
			throw new System.Exception(string.IsNullOrWhiteSpace(error) ? $"Ошибка регистрации: {(int)response.StatusCode}" : error);
		}
	}
}


