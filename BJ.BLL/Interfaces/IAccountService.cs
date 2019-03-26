using System.Threading.Tasks;
using ViewModels.AccountViews;

namespace BJ.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<LoginAccountResponseView> Login(LoginAccountView createAccountView);
        Task<RegistrationAccountResponseView> Register(RegisterAccountView updateAccount);
        Task<GetAllAccountResponseView> GetAll();
    }
}
