using ApiClient.Models;

namespace TeendokLista.MAUI.Services
{
    public sealed class LoggedUser : IUser
    {
        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string Szerepkor { get; set; }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }

        // Singleton minta
        // https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/
        private static readonly LoggedUser current = new LoggedUser();
        static LoggedUser()
        {
        }
        private LoggedUser()
        {
        }
        public static LoggedUser Current
        {
            get
            {
                return current;
            }
        }
    }
}