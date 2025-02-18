using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class CharacteristicsMaterialsContext : DbContext
    {
        public DbSet<CharacteristicsMaterials> CharacteristicsMaterials { get; set; }
        public CharacteristicsMaterialsContext()
        {
            Database.EnsureCreated();
            CharacteristicsMaterials.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
