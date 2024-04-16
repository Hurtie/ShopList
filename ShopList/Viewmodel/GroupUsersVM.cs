using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ShopList.Database;
using ShopList.Database.Objects;

namespace ShopList.Viewmodel
{
    [QueryProperty("Name", "Name")]
    public partial class GroupUsersVM: ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<User> users = [];
        [ObservableProperty]
        string name;
        public static int CreatorID { get; set; }
        public static int GroupID { get; set; }
        public GroupUsersVM() 
        {
            LoadUsers(GroupID);
        }

        async void LoadUsers(int groupId)
        {
            Users.Clear();
            List<User> userList = await Queries.GetGroupUsers(groupId);
            if (userList is not null)
            {
                foreach (User user in userList)
                {
                    Users.Add(user);
                }
            }
        }
    }
}
