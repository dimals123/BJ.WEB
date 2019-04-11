using BJ.DataAccess.Entities;

namespace BJ.BusinessLogic.Providers.Interfaces
{
    public interface IJwtTokenProvider
    {
        string GenerateJwtToken(User user);
    }
}
