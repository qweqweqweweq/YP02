using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class EquipmentContext : DbContext
    {
        public DbSet<Equipment> Equipment { get; set; }
        public EquipmentContext()
        {
            Database.EnsureCreated();
            Equipment.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
