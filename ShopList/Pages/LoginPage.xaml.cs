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
	    if(await _authService.Login(Login.Text, Password.Text))
		{
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			Login.Text = string.Empty;
			Password.Text = string.Empty;
        }
		else
		{
			await DisplayAlert("ќшибка", "¬ведЄн неверный логин или пароль", "OK");
		}
    }

	private async void Registration_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
	}
}