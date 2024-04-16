using ShopList.Viewmodel;

namespace ShopList
{
    public partial class MainPage : ContentPage
    {
        private MainVM vm;
        public MainPage()
        {
            InitializeComponent();
            vm = new MainVM();
            BindingContext = vm;
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            vm = new MainVM();
            BindingContext = vm;
        }
    }
}
