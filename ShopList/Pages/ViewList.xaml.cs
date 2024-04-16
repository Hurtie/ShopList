using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class ViewList : ContentPage
{
	ListVM vm;
	public ViewList()
	{
        InitializeComponent();
        vm = new ListVM();
		BindingContext = vm;
	}
}