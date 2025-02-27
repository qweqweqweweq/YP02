using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class ValueCharacteristicsContext : DbContext
    {
        public DbSet<ValueCharacteristics> ValueCharacteristics { get; set; }
        public ValueCharacteristicsContext()
        {
            Database.EnsureCreated();
            ValueCharacteristics.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
