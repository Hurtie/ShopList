using ShopList.Viewmodel;

namespace ShopList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainVM();
        }
    }
}
