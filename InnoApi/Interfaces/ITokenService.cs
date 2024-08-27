using InnoApi.Models;

namespace InnoApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUserModel user);
    }
}
