using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Database;
using ShopList.Database.Objects;
using ShopList.Pages;

namespace ShopList.Viewmodel
{
    [QueryProperty("ListName", "ListName")]
    public partial class ListVM: ObservableObject
    {
        [ObservableProperty]    
        string listName;
        [ObservableProperty]
        public static int listID;
        [ObservableProperty]
        ObservableCollection<Item> items = [];
        List<Database.Objects.ItemList> itemList = [];
        public ListVM()
        {
            LoadItems(listID);
        }
        public async void LoadItems(int listId)
        {
            itemList = await Queries.GetItems(listId);
            if (itemList is not null)
            {
                foreach (Database.Objects.ItemList item in itemList)
                {
                    Items.Add(new Item { Name = item.Name });
                }
            }
        }

        [RelayCommand]
        async Task Add()
        {
            var passItems = new Dictionary<string, object> { { "Items", Items } };
            NewItemVM.ListId = listID;
            await Shell.Current.GoToAsync($"{nameof(AddNewItem)}", passItems);
        }
        [RelayCommand]
        async Task Delete(Item s)
        {
            Items.Remove(s);
            await Queries.DeleteFromList(s.Id, listID);
        }
        [RelayCommand]
        async Task DeleteList()
        {
            await Queries.DeleteList(listID);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        [RelayCommand]
        async Task Tap(Item s)
        {
            string info = "";

            foreach (Database.Objects.ItemList item in itemList)
            {
                if (item.Name == s.Name)
                {
                    info = item.Info;
                    ItemVM.ListID = ListID;
                    ItemVM.ItemID = item.Id;
                    break;
                }
            }


            var passItems = new Dictionary<string, object> { { "ItemName", s.Name }, { "ItemInfo", info } };
            await Shell.Current.GoToAsync($"{nameof(ItemInfo)}", passItems);
        }
    }
}
