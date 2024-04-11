using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Viewmodel;

namespace ShopList.Pages;

public partial class AddNewItem : ContentPage
{
	ObservableCollection<string> found;
	NewItemVM vm = new NewItemVM();
	public AddNewItem()
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{
		found = new ObservableCollection<string>(NewItemVM.SearchItems(((SearchBar)sender).Text));
		CV.ItemsSource = found;
	}
	[RelayCommand]
	private async void CV_ItemSelected(string s)
	{
		if (vm.AddItem(s))
		{
			await DisplayAlert("Ошибка", "Товар уже есть в списке", "OK");
		}
		else
		{
			await Shell.Current.GoToAsync($"{nameof(ItemInfo)}?Item={s}");
		}
	}
    [RelayCommand]
    void Delete(string s)
    {
		
    }
}