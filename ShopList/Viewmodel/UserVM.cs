using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ShopList.Viewmodel
{
    public partial class UserVM: ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> groups;
        public UserVM()
        {
            Groups = ["Семья"];
        }
        [RelayCommand]
        void Delete(string s)
        {
            Groups.Remove(s);
        }
    }
}
