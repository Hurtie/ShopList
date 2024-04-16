using ShopList.Database;
using ShopList.Services;

namespace ShopList.Pages;

public partial class LoadingPage : ContentPage
{
    private readonly AuthService _authService;

    public LoadingPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        Preferences.Default.Remove("AuthState");

        if (await _authService.IsAuthenticatedAsync())
        {
            await Queries.LoadUser(Preferences.Default.Get("userID", ""));
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}