using BJ.BusinessLogic.Options;
using BJ.BusinessLogic.Services;
using BJ.DataAccess;
using BJ.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BJ.BusinessLogic.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void DependencyInjectionConnectionConfig(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionStringName = configuration.GetSection("DbOptions").Get<DbOptions>().ConnectionString;
            var connectionString = configuration.GetConnectionString(connectionStringName);
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
            else
            {
                throw new ApplicationException("Choose Dapper or EF");
            }


        }

        public static void DependencyInjectionServicesConfig(this IServiceCollection services)
        {

            services.Scan(scan => scan
                .FromAssemblyOf<AccountService>()
                   .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service") ||
                                                                type.Name.EndsWith("Provider") ||
                                                                type.Name.EndsWith("Helper")))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()

            );

            
        }

   
        
    }
}
