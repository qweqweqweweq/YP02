using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class DevelopersContext : DbContext
    {
        public DbSet<Developers> Developers { get; set; }
        public DevelopersContext()
        {
            Database.EnsureCreated();
            Developers.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
