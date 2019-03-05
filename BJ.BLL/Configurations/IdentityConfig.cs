using BJ.BLL.Options;
using BJ.DAL;
using BJ.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BJ.BLL.Configurations
{
    public static class IdentityConfig
    {
        public static void ConfigureIdentity(this IServiceCollection services)
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
        public static void ConfigureAutentification(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection("jwt").Get<JwtTokenOptions>();
            //configuration.Bind(jwtTokenOptions);
            //IOptions<JwtTokenOptions> option = Microsoft.Extensions.Options.Options.Create(new );
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
                    ValidIssuer = option.JwtIssuer,
                    ValidAudience = option.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.JwtKey)),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
            });
        }

    }
}
