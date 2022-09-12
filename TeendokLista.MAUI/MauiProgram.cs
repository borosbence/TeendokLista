using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Repositories;
using TeendokLista.MAUI.ViewModels;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI
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

            builder.Services.AddTransient<IFelhasznaloRepository, FelhasznaloLocalRepository>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>();

            builder.Services.AddTransient<IGenericRepository<Feladat>, FeladatLocalRepository>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<DetailViewModel>();
            builder.Services.AddTransient<DetailPage>();

            return builder.Build();
        }
    }
}