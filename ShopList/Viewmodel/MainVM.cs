using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Pages;
using ShopList.Database.Objects;
using System.Collections.ObjectModel;
using ShopList.Database;

namespace ShopList.Viewmodel
{
    [QueryProperty("List", "NewList")]
    public partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> lists = [];
        [ObservableProperty]
        ObservableCollection<string> groupNames = [];
        List<Database.Objects.Group> userGroups = [];
        List<Database.Objects.ShopList> shopLists = [];
        public MainVM()
        {
            LoadGroups(Queries.userData.Id);
        }
        public async void LoadLists(List<int> groupIds)
        {
            Lists.Clear();

            foreach (int id in groupIds)
            {
                List<Database.Objects.ShopList> SL = await Queries.GetLists(id);
                if (SL is not null)
                {
                    shopLists.AddRange(SL);
                }
            }

            if (shopLists is not null)
            {
                foreach (Database.Objects.ShopList list in shopLists)
                {
                    Lists.Add(list.Name);
                }
            }
        }

        public async void LoadGroups(int userId)
        {
            GroupNames.Clear();
            userGroups.Clear();

            userGroups = await Queries.GetGroups(userId);
            if (userGroups is not null)
            {
                foreach (Group group in userGroups)
                {
                    GroupNames.Add(group.Name);

                }

                List<int> groupIds = new List<int>();
                foreach (Database.Objects.Group group in userGroups)
                {
                    groupIds.Add(group.Id);
                }

                LoadLists(groupIds);
            }
        }

        [RelayCommand]
        async Task Tap(string s)
        {
            int id = 0;
            foreach (Database.Objects.ShopList list in shopLists)
            {
                if (list.Name == s)
                {
                    id = list.Id;
                    break;
                }
            }
            var send = new Dictionary<string, object> { { "ListName", s }};
            ListVM.listID = id;
            await Shell.Current.GoToAsync($"{nameof(ViewList)}", send);
        }
        [RelayCommand]
        async Task Add()
        {
            if (userGroups is null || userGroups.Count < 1)
            {
                await Application.Current.MainPage.DisplayAlert("Нет доступных групп", "Сначала создайте хотя бы одну группу", "ОК");
            }
            else
            {
                NewListVM.Groups = new ObservableCollection<Database.Objects.Group>(userGroups);
                await Shell.Current.GoToAsync($"{nameof(AddNewList)}");
            }
        }
        [RelayCommand]
        async Task Create()
        {
            await Shell.Current.GoToAsync($"{nameof(CreateNewGroup)}");
        }
        [RelayCommand]
        async Task Delete(string s)
        {
            Lists.Remove(s);
            int id = 0;
            foreach (Database.Objects.ShopList list in shopLists)
            {
                if (list.Name.Equals(s))
                {
                    id = list.Id;
                    break;
                }
            }
            await Queries.DeleteList(id);
        }
    }
}