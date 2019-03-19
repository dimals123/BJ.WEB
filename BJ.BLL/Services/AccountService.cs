using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BJ.BLL.Helpers;
using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using ViewModels.AccountViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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

        public async Task<string> FindById(string userId)
        {
            var result =await _userManager.FindByIdAsync(userId);
            return result.UserName;

        }

        public async Task<GetAllAccountView> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();
            var userNames = users.Select(x => x.UserName).ToList();

            var response = new GetAllAccountView();

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
                    Token = token
                };

                return response;
            
           
        }

        public async Task<LoginAccountResponseView> Register(RegisterAccountView model)
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
            var response = new LoginAccountResponseView()
            {
                Token = token
            };

            return response;

        }

        
    }
}
