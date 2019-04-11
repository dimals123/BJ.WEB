using System.Threading.Tasks;
using BJ.BusinessLogic.Services.Interfaces;
using BJ.ViewModels.AccountViews;
using Microsoft.AspNetCore.Mvc;


namespace BJ.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterAccountView model)
        {
            var result = await _accountService.Register(model);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginAccountView model)
        {
            var result = await _accountService.Login(model);
            return Ok(result);
        }

     

    

    }
}