using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class AddNewList : ContentPage
{
	public AddNewList()
	{
		InitializeComponent();
		BindingContext = new NewListVM();
	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
		NewListVM.SelectedID = PickGroup.SelectedIndex;
    }
}