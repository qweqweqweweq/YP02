using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class NapravlenieContext : DbContext
    {
        public DbSet<Napravlenie> Napravlenie { get; set; }
        public NapravlenieContext()
        {
            Database.EnsureCreated();
            Napravlenie.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
