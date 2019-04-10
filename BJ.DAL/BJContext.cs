using BJ.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BJ.DataAccess
{
    public class BJContext:IdentityDbContext<User>
    {
        public DbSet<Bot> Bots { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserInGame> UserInGames { get; set; }
        public DbSet<BotInGame> BotInGames { get; set; }
        public DbSet<UserStep> UserSteps { get; set; }
        public DbSet<BotStep> BotSteps { get; set; }

        public BJContext(DbContextOptions<BJContext> options):base(options)
        {

        }
    }
}
