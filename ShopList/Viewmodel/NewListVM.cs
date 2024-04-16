using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Database;
using ShopList.Database.Objects;

namespace ShopList.Viewmodel
{
    public partial class NewListVM: ObservableObject
    {
        [ObservableProperty]
        string newListName = string.Empty;

        [ObservableProperty]
        string placehold = "Название списка";

        public static ObservableCollection<Group> Groups;

        [ObservableProperty] 
        ObservableCollection<string> groupNames = [];

        public static int SelectedID { get; set; }

        public NewListVM()
        {
            foreach (Group group in Groups)
            {
                GroupNames.Add(group.Name);
            }
        }

        [RelayCommand]
        async Task AddList()
        { 
            if (string.IsNullOrWhiteSpace(NewListName))
            {
                return;
            }
            if (SelectedID == -1)
            {
                await Application.Current.MainPage.DisplayAlert("Не выбрана группа", "Выберите группу для которой создаёте список", "OK");
            }
            await Queries.CreateList(NewListName, Groups[SelectedID].Id);
            NewListName = string.Empty;
            Placehold = "Новый список добавлен!";
            await Task.Delay(1500);
            Placehold = "Название списка";
        }
    }
}
