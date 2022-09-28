namespace ApiClient.Models
{
    public class LoggedUser
    {
        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string Szerepkor { get; set; }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }

        private static readonly LoggedUser current = new LoggedUser();

        // https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/

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