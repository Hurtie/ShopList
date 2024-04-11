using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Pages;

namespace ShopList.Viewmodel
{
    [QueryProperty("ListName", "ListName")]
    [QueryProperty("Items", "Items")]
    public partial class ListVM: ObservableObject
    {
        [ObservableProperty]    
        string listName;
        [ObservableProperty]
        ObservableCollection<string> items;
        [RelayCommand]
        async Task Add()
        {
            var passItems = new Dictionary<string, object> { { "Items", Items } };

            await Shell.Current.GoToAsync($"{nameof(AddNewItem)}", passItems);
        }
        [RelayCommand]
        void Delete(string s)
        {
            Items.Remove(s);
        }
        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemInfo)}?Item={s}");
        }
    }
}
