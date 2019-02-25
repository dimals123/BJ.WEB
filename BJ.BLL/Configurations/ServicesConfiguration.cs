using BJ.BLL.Interfaces;
using BJ.BLL.Services;
using BJ.BLL.Tokens;
using BJ.DAL.Interfaces;
using BJ.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.BLL.Configurations
{
    public static class ServicesConfiguration
    {
        public static void InitServices(this IServiceCollection services)
        {
            services.AddTransient<IBotRepository, BotRepository>();
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IPointAccountRepository, PointAccountRepository>();
            services.AddTransient<IPointBotRepository, PointBotRepository>();
            services.AddTransient<IStepAccountRepository, StepAccountRepository>();
            services.AddTransient<IStepBotRepository, StepBotRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();


            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<JwtTokenHelper>();
        }
    }
}
