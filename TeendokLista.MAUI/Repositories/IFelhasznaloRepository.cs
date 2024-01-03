using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories
{
    public interface IFelhasznaloRepository
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}