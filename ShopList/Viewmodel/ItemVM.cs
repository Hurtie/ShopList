using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Database;

namespace ShopList.Viewmodel
{
    [QueryProperty("ItemName", "ItemName")]
    [QueryProperty("ItemInfo", "ItemInfo")]
    public partial class ItemVM : ObservableObject
    {
        [ObservableProperty]
        string itemName;
        [ObservableProperty]
        string itemInfo;
        public static int ItemID { get; set; }
        public static int ListID { get; set; }
        [RelayCommand]
        async Task Save()
        {
            if (!string.IsNullOrEmpty(ItemInfo))
            {
                await Queries.UpdateItem(ListID, ItemID, ItemInfo);
            }
        }
        [RelayCommand]
        async Task Delete()
        {
            await Queries.DeleteFromList(ItemID, ListID);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
