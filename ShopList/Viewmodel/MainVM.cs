using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Pages;
using System.Collections.ObjectModel;

namespace ShopList.Viewmodel
{
    [QueryProperty("List", "NewList")]
    public partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> list;
        ObservableCollection<string> Items;
        public MainVM()
        {
            List = ["Продукты", "Для печати"];
        }
        public void LoadList(string list)
        {
            switch (list)
            {
                case "Продукты":
                    Items = ["Молоко", "Сыр"];
                    break;

                case "Для печати":
                    Items = ["Бумага", "Чернила"];
                    break;

                default:
                    break;
            }
        }

        [RelayCommand]
        async Task Tap(string s)
        {
            LoadList(s);
            var send = new Dictionary<string, object> { { "ListName", s }, { "Items", Items } };
            await Shell.Current.GoToAsync($"{nameof(ViewList)}", send);
        }
        [RelayCommand]
        async Task Add()
        {
            var passList = new Dictionary<string, object> { { "List", List } };

            await Shell.Current.GoToAsync($"{nameof(AddNewList)}", passList);
        }
        [RelayCommand]
        void Delete(string s)
        {
            List.Remove(s);
        }
    }
}