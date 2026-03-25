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

            builder.Services.AddSingleton<ConnexionBD>();
            builder.Services.AddSingleton<TrajetViewModel>();
            builder.Services.AddSingleton<AddTrajetViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
