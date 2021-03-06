﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace BJ.BusinessLogic.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetAllErrorMessages(this ModelStateDictionary modelState)
        {

            var message = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return message;
        }

        public static string GetFirstErrorMessage(this ModelStateDictionary modelState)
        {

            var message = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
            return message;
        }


    }
}
