using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UserShell.Services;

namespace UserShell.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
	private readonly AuthApiService _authApiService;
	private readonly System.Action _switchToLogin;

	[ObservableProperty]
	private string userName = string.Empty;

	[ObservableProperty]
	private string password = string.Empty;

	[ObservableProperty]
	private string confirmPassword = string.Empty;

	[ObservableProperty]
	private string? errorMessage;

	[ObservableProperty]
	private bool isBusy;

	public RegisterViewModel(AuthApiService authApiService, System.Action switchToLogin)
	{
		_authApiService = authApiService;
		_switchToLogin = switchToLogin;
	}

	[RelayCommand]
	private async Task RegisterAsync()
	{
		ErrorMessage = null;
		if (Password != ConfirmPassword)
		{
			ErrorMessage = "Пароли не совпадают";
			return;
		}
		IsBusy = true;
		try
		{
			await _authApiService.RegisterAsync(UserName, Password);
			_switchToLogin();
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
	private void GoToLogin()
	{
		_switchToLogin();
	}
}


