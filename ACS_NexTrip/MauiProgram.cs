using ACS_NexTrip.Pages;
using ACS_NexTrip.Services;
using ACS_NexTrip.ViewModel;
using Microsoft.Extensions.Logging;

namespace ACS_NexTrip
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

            // --- SERVICES ---
            // Transient : chaque ViewModel reçoit sa propre connexion SQL
            // évite l'erreur "Un DataReader ouvert est déjà associé à Connection"
            builder.Services.AddTransient<ConnexionBD>();

            // --- VIEWMODELS ---
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<AddTrajetViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<TrajetViewModel>();
            builder.Services.AddSingleton<SettingsViewModel>();

            // --- PAGES ---
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<AddTrajetPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<TrajetPage>();
            builder.Services.AddSingleton<SettingsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}