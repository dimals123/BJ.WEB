using BJ.BusinessLogic.Helpers;
using BJ.BusinessLogic.Helpers.Interfaces;
using BJ.BusinessLogic.Providers;
using BJ.BusinessLogic.Providers.Interfaces;
using BJ.BusinessLogic.Services;
using BJ.BusinessLogic.Services.Interfaces;
using BJ.DataAccess;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BJ.BusinessLogic.Configurations
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
            services.AddTransient<IScoreHelper, ScoreHelper>();        
        }

   
        
    }
}
