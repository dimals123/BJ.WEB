using BJ.BLL.Helpers;
using BJ.BLL.Interfaces;
using BJ.BLL.Services;
using BJ.DAL;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BJ.BLL.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void InitServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BJContext>(options =>
                options.UseSqlServer(connection));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<JwtTokenProvider>();
        }
    }
}
