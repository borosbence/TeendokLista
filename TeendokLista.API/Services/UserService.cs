using System.Security.Claims;

namespace TeendokLista.API.Services
{
    public static class UserService
    {
        public static int GetUserId(ClaimsPrincipal User)
        {
            var claimId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (claimId != null)
            {
                return int.Parse(claimId.Value);
            }
            return 0;
        }
    }
}
