using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using JamCaturOffline.Views;
using JamCaturOffline.ViewModels;
using Plugin.MauiMTAdmob;

namespace JamCaturOffline;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit().UseMauiMTAdmob()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Inter-Bold.ttf", "InterBold");
                fonts.AddFont("Inter-SemiBold.ttf", "InterSemiBold");
                fonts.AddFont("Inter-Regular.ttf", "InterRegular");
            });

		builder.Services.AddSingleton<HomePage>();
		builder.Services.AddSingleton<HomePageVM>();

		builder.Services.AddTransient<Jam>();
		builder.Services.AddTransient<JamVM>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

