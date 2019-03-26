using BJ.BLL.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BJ.BLL.Filters
{
    public class ValidationActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                //var errorResult = new GenericResponseView<string>();
                //errorResult.Error = context.ModelState.GetFirstError();
                //context.Result =  new BadRequestObjectResult(errorResult);
                context.Result = new BadRequestObjectResult(context.ModelState.GetFirstErrorMessage());
            }

        }
    }
}
