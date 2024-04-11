using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class ViewList : ContentPage
{
	public ViewList()
	{
        InitializeComponent();
        BindingContext = new ListVM();
	}
}