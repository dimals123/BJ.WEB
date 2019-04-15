using BJ.BusinessLogic.Extensions;
using BJ.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BJ.BusinessLogic.Filters
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
                var errorResult = context.ModelState.GetFirstErrorMessage();
                context.Result = new BadRequestObjectResult(errorResult);
            }

        }
    }
}
