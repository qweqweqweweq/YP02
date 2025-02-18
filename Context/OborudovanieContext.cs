using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class OborudovanieContext : DbContext
    {
        public DbSet<Oborudovanie> Oborudovanie { get; set; }
        public OborudovanieContext()
        {
            Database.EnsureCreated();
            Oborudovanie.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
