using BJ.DAL.Entities;
using BJ.DAL.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BJ.BLL.Configurations
{
    public static class DbInitialize
    {
        public static async Task InitializeDb(this IServiceCollection services)
        {
            var servicesProvider = services.BuildServiceProvider();
            var unitOfWork = servicesProvider.GetService<IUnitOfWork>();

            var botsName = new string[] { "Olivia","Amelia","Isla","Emily","Ava","Lily","Mia", "Sofia", "Isabella","Grace",
                                                             "Oliver","Harry","Jack","George","Noah","Charlie", "Jacob","Alfie","Freddie","Oscar"};

            var bot = await unitOfWork.Bots.GetCount();

            if (bot == 0)
            {
                var bots = botsName.Select(x => new Bot
                {
                    Name = x
                }).ToList();

                await unitOfWork.Bots.CreateRange(bots);
            }



        }
    }
}
