using ShopList.Pages;

namespace ShopList
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ViewList), typeof(ViewList));
            Routing.RegisterRoute(nameof(AddNewList), typeof(AddNewList));
            Routing.RegisterRoute(nameof(AddNewItem), typeof(AddNewItem));
            Routing.RegisterRoute(nameof(ItemInfo), typeof(ItemInfo));
        }
    }
}
