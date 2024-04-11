using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ShopList.Viewmodel
{
    [QueryProperty("Items", "Items")]
    public partial class NewItemVM: ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> items;

        readonly static string[] all = ["Молоко", "Сыр", "Бумага", "Чернила", "Мел"];

        public bool AddItem(string item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);
                return false;
            }
            return true;
        }

        public static List<string> SearchItems(string text)
        {
            var found = all.Where(x => x.StartsWith(text, StringComparison.OrdinalIgnoreCase)).ToList();
            return found;
        }
    }
}
