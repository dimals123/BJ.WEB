using System.Threading.Tasks;
using BJ.BLL.Services.Interfaces;
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

        //protected async Task<IActionResult> Execute<TempType>(Func<Task<TempType>> func)
        //{
        //    GenericResponseView<TempType> response = new GenericResponseView<TempType>();
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var errorResult = new GenericResponseView<string>();
        //            errorResult.Error = ModelState.GetFirstError();
        //            return BadRequest(errorResult);
        //        }

        //        var result = await func();
        //        response.Model = result;
        //        return Ok(response);
        //    }
        //    catch(ValidationException ex)
        //    {
        //        response.Error = ex.Message;
        //        return BadRequest(response);
        //    }
        //}

        //protected async Task<IActionResult> Execute(Func<Task> func)
        //{
        //    var response = new GenericResponseView<string>();
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var errorResult = new GenericResponseView<string>();
        //            errorResult.Error = ModelState.GetFirstError();
        //            return BadRequest(errorResult);
        //        }

        //        await func();
        //        return Ok(response);
        //    }
        //    catch(ValidationException ex)
        //    {

        //        response.Error = ex.Message;
        //        return BadRequest(response);
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterAccountView registerAccountView)
        {
            var result = await _accountService.Register(registerAccountView);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginAccountView loginAccountView)
        {
            var result = await _accountService.Login(loginAccountView);
            return Ok(result);
        }

     

    

    }
}