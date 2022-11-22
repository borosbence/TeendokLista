namespace TeendokLista.MAUI.Services
{
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static string FelhasznaloNev { get; set; }
        public static string Szerepkor { get; set; }

        // Ez az értékéket TokenAuthHandler már nem használja
        public static string Access_Token { get; set; }
        public static string Refresh_Token { get; set; }
    }
}