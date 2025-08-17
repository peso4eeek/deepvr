using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UserShell.Services;

namespace UserShell.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
	private readonly AuthApiService _authApiService = new();

	[ObservableProperty]
	private ViewModelBase? currentViewModel;

	public MainWindowViewModel()
	{
		NavigateToLogin();
	}

	[RelayCommand]
	private void NavigateToLogin()
	{
		CurrentViewModel = new LoginViewModel(
			_authApiService,
			onLoggedIn: OnLoggedIn,
			switchToRegister: NavigateToRegister
		);
	}

	[RelayCommand]
	private void NavigateToRegister()
	{
		CurrentViewModel = new RegisterViewModel(
			_authApiService,
			switchToLogin: NavigateToLogin
		);
	}

	[RelayCommand]

	private void NavigateToMetrics()
	{
		CurrentViewModel = new MetricsViewModel();
	}

	private void OnLoggedIn(string accessToken, string refreshToken)
	{
		NavigateToMetrics();
	}
}