using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ShopList.Database.Objects;
using CommunityToolkit.Mvvm.Input;
using ShopList.Database;

namespace ShopList.Viewmodel
{
    public partial class NewGroupVM: ObservableObject
    {
        [ObservableProperty]
        string newGroupName;

        [ObservableProperty]
        string placehold = "Название группы";

        [RelayCommand]
        async Task AddGroup()
        {
            if (string.IsNullOrWhiteSpace(NewGroupName))
            {
                return;
            }
            await Queries.CreateGroup(NewGroupName, Queries.userData.Id);
            NewGroupName = string.Empty;
            Placehold = "Новая группа создана!";
            await Task.Delay(1500);
            Placehold = "Название группы";
        }
    }
}
