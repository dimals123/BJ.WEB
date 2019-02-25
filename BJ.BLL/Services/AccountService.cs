using System;
using System.Threading.Tasks;
using BJ.BLL.Interfaces;
using BJ.BLL.Tokens;
using BJ.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BJ.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly JwtTokenHelper _jwtTokentHelper;

        public AccountService()
        {

        }

        public Task Create(CreateAccountView createAccountView)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(UpdateAccount updateAccount)
        {
            throw new NotImplementedException();
        }
    }
}
