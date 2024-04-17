using ShopList.Database;
using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class UsersInGroup : ContentPage
{
	GroupUsersVM vm;
	public UsersInGroup()
	{
		InitializeComponent();
		vm = new GroupUsersVM();
		BindingContext = vm;
		if (Queries.userData.Id == GroupUsersVM.CreatorID)
		{
			this.Button.Text = "������� ������";
		}
		else
		{
			this.Button.Text = "����� �� ������";
		}
	}

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
		string res = await DisplayPromptAsync("���������� ������������", "������� ID ������������, �������� ������ ��������");
		bool parse = int.TryParse(res, out int id);
		if (!parse)
		{
			await DisplayAlert("������", "������� ����� ID", "��");
		}
		else
		{
			await Queries.AddUserToGroup(id, GroupUsersVM.GroupID);
			vm.Users = new System.Collections.ObjectModel.ObservableCollection<Database.Objects.User>(await Queries.GetGroupUsers(GroupUsersVM.GroupID));
		}
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		if(this.Button.Text == "������� ������")
		{
			await Queries.DeleteGroup(GroupUsersVM.GroupID);
		}
		else
		{
			await Queries.DeleteFromGroup(GroupUsersVM.GroupID);
		}
		await Shell.Current.GoToAsync($"//{nameof(UserPage)}");
    }
}