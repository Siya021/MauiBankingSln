using MauiBankingExercise.Services;
using MauiBankingExercise.ViewModels;
using MauiBankingExercise.Views;
using Microsoft.Extensions.Logging;

namespace MauiBankingExercise
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Register services
            builder.Services.AddSingleton<BankingDatabaseService>();

            builder.Services.AddTransient<CustomerDashboard>();
            builder.Services.AddTransient<CustomerDashboardViewModel>();

            builder.Services.AddTransient<CustomerSelectionView>();
            builder.Services.AddTransient<CustomerSelectionViewModel>();


            return builder.Build();
        }
    }
}
