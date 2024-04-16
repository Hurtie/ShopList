using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ShopList.Database;
using ShopList.Database.Objects;

namespace ShopList.Viewmodel
{
    [QueryProperty("Items", "Items")]
    public partial class NewItemVM: ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Item> items;
        public static int ListId { get; set; }
        static List<Item> all = [];
        public NewItemVM()
        {
            LoadItems();
        }
        static async void LoadItems()
        {
            all.Clear();
            List<Item> res = await Queries.LoadItems();
            if (res is not null)
            {
                all = res;
            }
        }
        public async Task<bool> AddItem(Item item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);
                await Queries.AddItemToList(ListId, item.Id);
                ItemVM.ItemID = item.Id;
                ItemVM.ListID = ListId;
                return false;
            }
            return true;
        }

        public static List<Item> SearchItems(string text)
        {
            var found = all.Where(x => x.Name.StartsWith(text, StringComparison.OrdinalIgnoreCase)).ToList();
            return found;
        }
    }
}
