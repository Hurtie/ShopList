using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ShopList.Viewmodel
{
    [QueryProperty("List", "List")]
    public partial class NewListVM: ObservableObject
    {
        [ObservableProperty]
        string newListName;

        [ObservableProperty]
        string placehold = "Название";

        [ObservableProperty]
        ObservableCollection<string> list;

        [RelayCommand]
        async void AddList()
        { 
            if (string.IsNullOrWhiteSpace(NewListName))
            {
                return;
            }
            List.Add(NewListName);
            NewListName = string.Empty;
            Placehold = "Новый список добавлен!";
            await Task.Delay(1500);
            Placehold = "Название";
        }
    }
}
