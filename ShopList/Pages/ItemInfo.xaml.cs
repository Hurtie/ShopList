using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class ItemInfo : ContentPage
{
	public ItemInfo()
	{
		InitializeComponent();
		BindingContext = new ItemVM();
	}
}