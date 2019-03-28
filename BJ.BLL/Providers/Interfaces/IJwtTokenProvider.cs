using BJ.DAL.Entities;

namespace BJ.BLL.Providers.Interfaces
{
    public interface IJwtTokenProvider
    {
        string GenerateJwtToken(User user);
    }
}
