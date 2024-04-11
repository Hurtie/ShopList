using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class AddNewList : ContentPage
{
	public AddNewList()
	{
		InitializeComponent();
		BindingContext = new NewListVM();
	}
}