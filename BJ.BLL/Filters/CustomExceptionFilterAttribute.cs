using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BJ.BLL.Filters
{
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionMessage = context.Exception.Message;
            context.Result = new ContentResult
            {
                Content = $"В методе {actionName} возникло исключение: \n {exceptionMessage}"
            };
            context.ExceptionHandled = true;
        }
    }
}
