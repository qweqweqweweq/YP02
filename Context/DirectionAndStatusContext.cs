using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class DirectionAndStatusContext : DbContext
    {
        public DbSet<DirectionAndStatus> DirectionAndStatus { get; set; }
        public DirectionAndStatusContext()
        {
            Database.EnsureCreated();
            DirectionAndStatus.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
