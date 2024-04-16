using Microsoft.Extensions.Logging;
using ShopList.Pages;
#if ANDROID
using ShopList.Platforms.Android;
#endif
using ShopList.Services;

namespace ShopList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if ANDROID
                builder.ConfigureMauiHandlers(handlers => {

                    handlers.AddHandler<CustomViewCell, CustomViewCellHandler>();

        }); 
#endif
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<UserPage>();

            return builder.Build();
        }
    }
}
