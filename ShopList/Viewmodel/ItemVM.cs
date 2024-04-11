using CommunityToolkit.Mvvm.ComponentModel;

namespace ShopList.Viewmodel
{
    [QueryProperty("Item", "Item")]
    public partial class ItemVM : ObservableObject
    {
        [ObservableProperty]
        string _item;
    }
}
