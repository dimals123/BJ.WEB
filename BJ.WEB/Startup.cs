using BJ.BLL.Configurations;
using BJ.BLL.Filters;
using BJ.BLL.Middleware;
using BJ.BLL.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace BJ.WEB
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InitServices(Configuration);
            services
                .AddMvc(options => options.Filters.Add(typeof(ValidationActionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureIdentity();
            services.AddRouting();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.Configure<JwtTokenOptions>(Configuration.GetSection("jwt"));
            services.ConfigureAutentification(Configuration);

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseHsts();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware(typeof(ExceptionsMiddleware));
            app.UseMvc(routes => routes.MapRoute("default", "{controller}/{action}"));

            app.UseSpa(spa =>
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
