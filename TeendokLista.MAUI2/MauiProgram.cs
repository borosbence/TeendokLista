using ApiClient.MAUI.Handlers;
using ApiClient.Repositories;
using Microsoft.Extensions.DependencyInjection;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Repositories;
using TeendokLista.MAUI.Repositories.API;
using TeendokLista.MAUI.Repositories.Local;
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
            builder.Services.AddSingleton<CurrentUser>();
            //builder.Services.AddScoped<IFelhasznaloRepository, FelhasznaloLocalRepository>();

            //var app = builder.Build();
            //var cu = app.Services.GetRequiredService<CurrentUser>();
            // A singleton példány átadása a többi szolgáltatásnak az alkalamzás indításakor
            var provider = builder.Services.BuildServiceProvider();
            var cu = provider.GetRequiredService<CurrentUser>();

            builder.Services.AddScoped<IFelhasznaloRepository, FelhasznaloAPIRepository>(x =>
            {
                 return new(cu, "api/token");
            });
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>();
            // A tesztelés miatt kell a Singleton a FeladatLocalRepository-nál!
            //builder.Services.AddSingleton<IGenericRepository<Feladat>, FeladatLocalRepository>();
            builder.Services.AddScoped<IGenericRepository<Feladat>, GenericAPIRepository<Feladat>>(x =>
            {
                return new("api/feladatok", handler: new TokenAuthHandler("api/token/refresh", cu.AccessToken, cu.RefreshToken));
            });
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<DetailViewModel>();
            builder.Services.AddTransient<DetailPage>();

            return builder.Build();
        }
    }
}