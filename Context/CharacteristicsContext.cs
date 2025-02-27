using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class CharacteristicsContext : DbContext
    {
        public DbSet<Characteristics> Characteristics { get; set; }
        public CharacteristicsContext()
        {
            Database.EnsureCreated();
            Characteristics.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
