using BJ.BLL.Options;
using BJ.DAL;
using BJ.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BJ.BLL.Configurations
{
    public static class IdentityConfig
    {
        public static void IdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;

            })
            .AddEntityFrameworkStores<BJContext>()
            .AddDefaultTokenProviders();


        }
        public static void AutentificationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection("jwt").Get<JwtTokenOptions>();
            //var jwt = new JwtTokenOptions();
            //configuration.Bind(jwt);
            //IOptions<JwtTokenOptions> option = Microsoft.Extensions.Options.Options.Create(jwt);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {

                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = option.Issuer,
                    ValidAudience = option.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.Key)),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
            });
        }

    }
}
