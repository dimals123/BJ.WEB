using BJ.BusinessLogic.Helpers;
using BJ.BusinessLogic.Helpers.Interfaces;
using BJ.BusinessLogic.Options;
using BJ.BusinessLogic.Providers;
using BJ.BusinessLogic.Providers.Interfaces;
using BJ.BusinessLogic.Services;
using BJ.BusinessLogic.Services.Interfaces;
using BJ.DataAccess;
using BJ.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace BJ.BusinessLogic.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void DependencyInjectionConnectionConfig(this IServiceCollection services, IConfiguration configuration)
        {

            var titleConnectionString = configuration.GetSection("DbOptions").Get<DbOptions>().ConnectionString;
            var connectionString = configuration.GetConnectionString(titleConnectionString);
            services.AddDbContext<BJContext>(options => options.UseSqlServer(connectionString));


            var dbOptions = configuration.GetSection("DbOptions").Get<DbOptions>();

            if(dbOptions.ORM == "EF")
            {
                
                services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            }
            else if(dbOptions.ORM == "Dapper")
            {
                services.AddTransient<IDbConnection>(db => new SqlConnection(connectionString));
                services.AddTransient<IUnitOfWork, DapperUnitOfWork>();
            }

        }

        public static void DependencyInjectionServicesConfig(this IServiceCollection services)
        {
            
            
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();
            services.AddTransient<IScoreHelper, ScoreHelper>();
            services.AddTransient<ICardsHelper, CardsHelper>();
        }

   
        
    }
}
