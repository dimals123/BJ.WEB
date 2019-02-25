using System;
using System.Threading.Tasks;

namespace BJ.BLL.Interfaces
{
    public interface IAccountService
    {
        Task Create(CreateAccountView createAccountView);
        Task Delete(Guid id);
        Task Update(UpdateAccount updateAccount);
    }
}
