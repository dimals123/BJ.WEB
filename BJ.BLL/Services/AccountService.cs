using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BJ.BLL.Helpers;
using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using ViewModels.AccountViews;

namespace BJ.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtTokenHelper _jwtTokentHelper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, JwtTokenHelper jwtTokentHelper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokentHelper = jwtTokentHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllAccountView> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();
            var userNames = users.Select(x => x.UserName).ToList();

            GetAllAccountView response = new GetAllAccountView();
            foreach (var name in userNames)
            {
                response.AccountNames.Add(new AccountGetAllAccountViewItem { Name = name });
            }
            return response;
           
        }

        public async Task<LoginAccountResponseView> Login(LoginAccountView model)
        {
            var result = await _userManager.FindByNameAsync(model.Name);
            var ver = await _signInManager.UserManager.CheckPasswordAsync(result, model.Password);
            if (!ver)
            {
                throw new ValidationException("Invalid login attempt");
            }


            var token = _jwtTokentHelper.GenerateJwtToken(model.Name, result);
            var response = new LoginAccountResponseView()
            {
                Token = token
            };

            return response;
        }

        public async Task<LoginAccountResponseView> Register(RegisterAccountView model)

        {
            var Account = new User
            {
                UserName = model.Name,
            };
            var result = await _userManager.CreateAsync(Account, model.Password);
            if (!result.Succeeded)
            {
                throw new ValidationException("Invalid user");
            }

            var token = _jwtTokentHelper.GenerateJwtToken(model.Name, Account);
            var response = new LoginAccountResponseView()
            {
                Token = token
            };

            return response;

        }
    }
}
