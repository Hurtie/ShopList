using ShopList.Database;
using ShopList.Database.Objects;
using ShopList.Pages;
using ShopList.Services;
using ShopList.Viewmodel;
using Word = Microsoft.Office.Interop.Word;

namespace ShopList;

public partial class UserPage : ContentPage
{
	private readonly AuthService _authService;
	public UserPage(AuthService authService)
	{
		InitializeComponent();
        BindingContext = new UserVM();
		_authService = authService;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = new UserVM();
        name.Text = Queries.userData.Name;
        ID.Text = $"ID: {Queries.userData.Id}";
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		_authService.Logout();
		Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Удаление аккканута", "Вы действительно хотите удалить свой аккаунт?", "Удалить", "Отмена");
        if (answer)
        {
            _authService.Logout();
            await Queries.DeleteUser();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
    private async void Results_Clicked(object sender, EventArgs e)
    {
        List<Results> res = await Queries.Results();
        string curGroup = "";
        string curList = "";

        var word = new Word.Application();

        Word.Document doc = word.Documents.Add();

        Word.Paragraph user = doc.Paragraphs.Add();
        Word.Range userRange = user.Range;
        userRange.Text = $"{Queries.userData.Name}\n";
        userRange.InsertParagraphAfter();

        foreach (Results r in res)
        {
            if (curGroup != r.Group)
            {
                Word.Paragraph group = doc.Paragraphs.Add();
                Word.Range groupRange = group.Range;
                groupRange.Text = $"- {r.Group}";
                doc.Paragraphs.Add();
                curGroup = r.Group;
            }
            if (curList != r.List)
            {
                Word.Paragraph list = doc.Paragraphs.Add();
                Word.Range listRange = list.Range;
                listRange.Text = $"\t- {r.List}";
                doc.Paragraphs.Add();
                curList = r.List;
            }
            Word.Paragraph item = doc.Paragraphs.Add();
            Word.Range itemRange = item.Range;
            itemRange.Text = $"\t\t- {r.Item}: '{r.Info}'\n";
        }

        string savePath = "";

#if ANDROID
        savePath = @"/storage/emulated/0/Download/result.docx";
#endif
#if WINDOWS
        savePath = @"result.docx";
#endif
        doc.SaveAs2(savePath);
        doc.Close();
        await DisplayAlert("Создан отчёт", $"Отчёт сформирован", "OK");
    }
}