using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UserShell.Services;

namespace UserShell.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
	private readonly AuthApiService _authApiService;
	private readonly System.Action<string, string> _onLoggedIn;
	private readonly System.Action _switchToRegister;

	[ObservableProperty]
	private string userName = string.Empty;

	[ObservableProperty]
	private string password = string.Empty;

	[ObservableProperty]
	private string? errorMessage;

	[ObservableProperty]
	private bool isBusy;

	public LoginViewModel(AuthApiService authApiService, System.Action<string, string> onLoggedIn, System.Action switchToRegister)
	{
		_authApiService = authApiService;
		_onLoggedIn = onLoggedIn;
		_switchToRegister = switchToRegister;
	}

	[RelayCommand]
	private async Task LoginAsync()
	{
		ErrorMessage = null;
		IsBusy = true;
		try
		{
			var authResponse = await _authApiService.LoginAsync(UserName, Password);
			_onLoggedIn(authResponse.AccessToken, authResponse.RefreshToken);
		}
		catch (System.Exception ex)
		{
			ErrorMessage = ex.Message;
		}
		finally
		{
			IsBusy = false;
		}
	}

	[RelayCommand]
	private void GoToRegister()
	{
		_switchToRegister();
	}
}


