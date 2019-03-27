using BJ.DAL.Entities;

namespace BJ.BLL.Helpers.Providers.Interfaces
{
    public interface IJwtTokenProvider
    {
        string GenerateJwtToken(string name, User user);
    }
}
