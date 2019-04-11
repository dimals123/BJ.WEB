using BJ.BusinessLogic.Configurations;
using BJ.BusinessLogic.Extensions;
using BJ.BusinessLogic.Filters;
using BJ.BusinessLogic.Middleware;
using BJ.BusinessLogic.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace BJ.WEB
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.DependencyInjectionConnectionConfig(Configuration);
            services.DependencyInjectionServicesConfig();
            services
                .AddMvc(options => options.Filters.Add(typeof(ValidationActionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        
            services.IdentityConfigure();
            services.AddRouting();

            services.InitializeDbBots();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); 
            services.Configure<JwtTokenOptions>(Configuration.GetSection("jwt"));
            services.AutentificationConfigure(Configuration);
            services.AddCors(); 

            services.AddSpaStaticFiles(configuration =>
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    configuration.RootPath = "ClientApp/dist/ClientApp";
                }
                else
                {
                    configuration.RootPath = "wwwroot";
                }
            });
        

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseHsts();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(builder=>
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware(typeof(ExceptionsMiddleware));
            app.UseMvc(routes => routes.MapRoute("default", "{controller}/{action}"));

            app.UseSpa((spa) =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
