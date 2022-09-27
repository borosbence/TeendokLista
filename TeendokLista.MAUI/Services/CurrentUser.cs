using System.Text.Json.Serialization;

namespace TeendokLista.MAUI.Services
{
    // TODO: Singletonná alakítás?
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static string FelhasznaloNev { get; set; }
        public static string Szerepkor { get; set; }
        public static string Access_Token { get; set; }
        public static string Refresh_Token { get; set; }
    }
}