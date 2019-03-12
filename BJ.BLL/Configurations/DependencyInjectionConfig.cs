using BJ.BLL.Helpers;
using BJ.BLL.Interfaces;
using BJ.BLL.Services;
using BJ.DAL;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace BJ.BLL.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void InitServices(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BJContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<JwtTokenProvider>();
        }
    }
}
