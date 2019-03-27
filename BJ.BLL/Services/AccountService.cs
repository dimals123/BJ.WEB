using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BJ.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using BJ.ViewModels.AccountViews;
using BJ.DAL.Repositories.Interfaces;
using BJ.BLL.Helpers.Providers;
using BJ.BLL.Services.Interfaces;

namespace BJ.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtTokenProvider _jwtTokentHelper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, JwtTokenProvider jwtTokentHelper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokentHelper = jwtTokentHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllAccountResponseView> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();
            var userNames = users.Select(x => x.UserName).ToList();

            var response = new GetAllAccountResponseView();

            response.AccountNames = userNames;
           
           


            return response;
           
        }

        public async Task<LoginAccountResponseView> Login(LoginAccountView model)
        {
          
                var result = await _userManager.FindByNameAsync(model.Name);
                var identityResult = await _signInManager.UserManager.CheckPasswordAsync(result, model.Password);
                if (!identityResult)
                {
                    throw new ValidationException("Invalid login attempt");
                }


                var token = _jwtTokentHelper.GenerateJwtToken(model.Name, result);
                var response = new LoginAccountResponseView()
                {
                    UserId = result.Id,
                    Token = token
                };

                return response;
            
           
        }

        public async Task<RegistrationAccountResponseView> Register(RegisterAccountView model)
        {
            var account = new User
            {
                UserName = model.Name,
            };
            var result = await _userManager.CreateAsync(account, model.Password);
            if (!result.Succeeded)
            {
                throw new ValidationException("Invalid user");
            }

            var token = _jwtTokentHelper.GenerateJwtToken(model.Name, account);
            var response = new RegistrationAccountResponseView()
            {
                UserId = account.Id,
                Token = token
            };

            return response;

        }
        
    }
}
