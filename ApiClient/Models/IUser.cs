namespace ApiClient.Models
{
    public interface IUser
    {
        string Access_Token { get; set; }
        string Refresh_Token { get; set; }
    }
}
