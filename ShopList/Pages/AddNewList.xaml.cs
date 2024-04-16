using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class AddNewList : ContentPage
{
	NewListVM vm;
	public AddNewList()
	{
		InitializeComponent();
		vm = new NewListVM();
		BindingContext = vm;
	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
		NewListVM.SelectedID = PickGroup.SelectedIndex;
    }
}