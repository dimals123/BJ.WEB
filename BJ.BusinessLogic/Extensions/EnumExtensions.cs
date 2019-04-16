using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BJ.BusinessLogic.Extensions
{
    public static class EnumExtensions
    {
        public static int GetDescription(this Enum value)
        {
            var response =  value.GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
            return Convert.ToInt32(response);
        }

    }
}

