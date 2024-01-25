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


            // Az egymástól függő osztályok regisztrálása
            // builder.Services.AddTransient<IFelhasznaloRepository, FelhasznaloLocalRepository>();
            builder.Services.AddTransient<IFelhasznaloRepository, FelhasznaloAPIRepository>(x =>
            {
                return new("api/token");
            });
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>();

            // A tesztelés miatt kell a Singleton a FeladatLocalRepository-nál!
            //builder.Services.AddSingleton<IGenericRepository<Feladat>, FeladatLocalRepository>();
            builder.Services.AddTransient<IGenericRepository<FeladatModel>, GenericAPIRepository<FeladatModel>>(x =>
            {
                return new("api/feladatok", handler: new TokenAuthHandler("api/token/refresh", CurrentUser.AccessToken!, CurrentUser.RefreshToken!));
            });
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<DetailViewModel>();
            builder.Services.AddTransient<DetailPage>();

            return builder.Build();
        }
    }
}