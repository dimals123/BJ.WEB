using System.Threading.Tasks;
using BJ.ViewModels.AccountViews;

namespace BJ.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginAccountResponseView> Login(LoginAccountView createAccountView);
        Task<RegistrationAccountResponseView> Register(RegisterAccountView updateAccount);
        Task<GetAllAccountResponseView> GetAll();
    }
}
