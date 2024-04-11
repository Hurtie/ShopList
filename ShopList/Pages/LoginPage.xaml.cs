using ShopList.Services;

namespace ShopList.Pages;

public partial class LoginPage : ContentPage
{
	private readonly AuthService _authService;
	public LoginPage(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
	    if(_authService.Login(Login.Text, Password.Text))
		{
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			Login.Text = string.Empty;
			Password.Text = string.Empty;
        }
		else
		{
			await DisplayAlert("ќшибка", "¬веЄден неверный логин или пароль", "OK");
		}
    }
}