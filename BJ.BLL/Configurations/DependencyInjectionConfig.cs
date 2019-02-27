using BJ.BLL.Helpers;
using BJ.BLL.Interfaces;
using BJ.BLL.Services;
using BJ.DAL;
using BJ.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BJ.BLL.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void InitServices(this IServiceCollection services)
        { 
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<JwtTokenHelper>();
        }
    }
}
