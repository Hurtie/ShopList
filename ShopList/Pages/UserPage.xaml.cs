using ShopList.Database;
using ShopList.Pages;
using ShopList.Services;
using ShopList.Viewmodel;

namespace ShopList;

public partial class UserPage : ContentPage
{
	private readonly AuthService _authService;
	public UserPage(AuthService authService)
	{
		InitializeComponent();
        BindingContext = new UserVM();
		_authService = authService;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = new UserVM();
        name.Text = Queries.userData.Name;
        ID.Text = $"ID: {Queries.userData.Id}";
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		_authService.Logout();
		Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}