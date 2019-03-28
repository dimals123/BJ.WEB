using System.Threading.Tasks;
using BJ.ViewModels.AccountViews;

namespace BJ.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginAccountResponseView> Login(LoginAccountView model);
        Task<RegistrationAccountResponseView> Register(RegisterAccountView model);
        Task<GetAllAccountResponseView> GetAll();
    }
}
