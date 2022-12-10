using ApiClient.MAUI.Handlers;
using ApiClient.Repositories;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Repositories;
using TeendokLista.MAUI.Repositories.API;
using TeendokLista.MAUI.Repositories.Local;
using TeendokLista.MAUI.Services;
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

            //builder.Services.AddScoped<IFelhasznaloRepository, FelhasznaloLocalRepository>();
            builder.Services.AddScoped<IFelhasznaloRepository, FelhasznaloAPIRepository>(x =>
            {
                return new("api/token");
            });
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>();
            //builder.Services.AddScoped<IGenericRepository<Feladat>, FeladatLocalRepository>();
            builder.Services.AddScoped<IGenericRepository<Feladat>, GenericAPIRepository<Feladat>>(x =>
            {
                return new("api/feladatok", handler: new TokenAuthHandler("api/token", CurrentUser.Access_Token, CurrentUser.Refresh_Token));
            });
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<DetailViewModel>();
            builder.Services.AddTransient<DetailPage>();

            return builder.Build();
        }
    }
}