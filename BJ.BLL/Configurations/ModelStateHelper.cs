using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace BJ.BLL.Configurations
{
    public static class ModelStateHelper
    {
        public static List<string> Errors(this ModelStateDictionary modelState)
        {

            var message = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return message;
        }

        public static string Error(this ModelStateDictionary modelState)
        {

            var message = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
            return message;
        }
    }
}
