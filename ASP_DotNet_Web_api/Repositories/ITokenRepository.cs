using Microsoft.AspNetCore.Identity;

namespace ASP_DotNet_Web_api.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<String> roles);
    }
}
