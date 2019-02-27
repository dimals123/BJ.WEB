using BJ.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BJ.DAL
{
    public class BJContext:IdentityDbContext<User>
    {
        public DbSet<Bot> Bots { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PointAccount> PointsAccounts { get; set; }
        public DbSet<PointBot> BotsAccounts { get; set; }
        public DbSet<StepAccount> StepsAccounts { get; set; }
        public DbSet<StepBot> StepsBots { get; set; }

        public BJContext(DbContextOptions<BJContext> options):base(options)
        {

        }
    }
}
