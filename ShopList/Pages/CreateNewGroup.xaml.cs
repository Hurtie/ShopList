using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class CreateNewGroup : ContentPage
{
	public CreateNewGroup()
	{
		InitializeComponent();
		BindingContext = new NewGroupVM(); 
	}
}