using System.Text.RegularExpressions;
using ShopList.Database;

namespace ShopList.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string loginPattern = @"^[a-zA-Z][a-zA-Z0-9-_.]{1,20}$";
        string passPattern = @"(?=^.{8,}$)^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$";

        try
        {
            if ("" != await Queries.CheckUserAsync(Login.Text))
            {
                throw new Exception("Пользователь с данным логином уже зарегистриван");
            }
            if (!Regex.IsMatch(Login.Text, loginPattern))
            {
                throw new Exception("Ограничения логина:\n- 2-20 символов\n- Буквы латинского алфавита или цифры\n- Первый символ обязательно буква");
            }
            if (!Regex.IsMatch(Password.Text, passPattern))
            {
                throw new Exception("Пароль должен содержать от 8 символов:\n- Строчных и заглавных латинских букв\n- Цифр");
            }
            if (string.IsNullOrEmpty(Name.Text) || Name.Text.Length < 5)
            {
                throw new Exception("Слишком короткое имя пользователя, введите хотя бы 5 символов");
            }
            await Queries.RegisterUser(Login.Text, Password.Text, Name.Text);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            Login.Text = string.Empty;
            Password.Text = string.Empty;
            Name.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "Закрыть");
        }
    }
}