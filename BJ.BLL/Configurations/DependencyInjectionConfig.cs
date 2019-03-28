using BJ.BLL.Providers;
using BJ.BLL.Providers.Interfaces;
using BJ.BLL.Services;
using BJ.BLL.Services.Interfaces;
using BJ.DAL;
using BJ.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BJ.BLL.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void DbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BJContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void InitServices(this IServiceCollection services)
        {
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();
        
        }

   
        
    }
}
