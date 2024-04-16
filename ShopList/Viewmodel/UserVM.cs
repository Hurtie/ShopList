using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Database;
using ShopList.Pages;

namespace ShopList.Viewmodel
{
    public partial class UserVM: ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> groupNames = [];
        public List<Database.Objects.Group> userGroups;
        public UserVM()
        {
            LoadGroups(Queries.userData.Id);
        }

        public async void LoadGroups(int userId)
        {
            userGroups = await Queries.GetGroups(userId);
            if (userGroups is not null)
            {
                foreach (Database.Objects.Group group in userGroups)
                {
                    GroupNames.Add(group.Name);
                }
            }
        }
        [RelayCommand]
        async Task Tap(string s)
        {
            int[] ids = new int[2];
            foreach (Database.Objects.Group group in userGroups)
            {
                if (group.Name == s)
                {
                    ids[0] = group.Id;
                    ids[1] = group.Creator_Id;
                    break;
                }
            }
            GroupUsersVM.GroupID = ids[0];
            GroupUsersVM.CreatorID = ids[1];
            await Shell.Current.GoToAsync($"{nameof(UsersInGroup)}?Name={s}");
        }
    }
}
