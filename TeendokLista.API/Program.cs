using JwtSecurity.Extensions;
using Microsoft.EntityFrameworkCore;
using TeendokLista.API.Data;

namespace TeendokLista.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Ezzel kívülrõl is elérhetõ az API
            builder.WebHost.UseUrls("http://localhost:5000", "http://0.0.0.0:5000");

            // DbContext
            builder.Services.AddDbContext<TeendokContext>(options =>
            options.UseMySql(
                    builder.Configuration.GetConnectionString("TeendokDB"),
                    ServerVersion.Parse("10.4.24-mariadb"))
            );

            // Add services to the container.
            builder.Services.AddControllers();

            // Szolgáltatások hozzáadása
            builder.RegisterJWTAuthentication();

            var app = builder.Build();

            // Fontos a sorrend!
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}