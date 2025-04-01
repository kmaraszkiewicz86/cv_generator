using System.Globalization;
using Microsoft.Extensions.Logging;
using CvGenerator.DependencyInjection;
using CvGenerator.Extensions;

namespace CvGenerator;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
        string uiLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.ConfigureDatabaseLogic($"Data Source={dbPath}")
            .AddCqrsHandlers()
            .AddViewModels()
            .AddViews()
            .AddServices()
            .ConfigureDefaultLanguage(uiLanguage)
            .AddValidators();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
