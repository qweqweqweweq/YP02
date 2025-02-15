using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class TypeEquipmentContext : DbContext
    {
        public DbSet<TypeEquipment> TypeEquipment { get; set; }
        public TypeEquipmentContext()
        {
            Database.EnsureCreated();
            TypeEquipment.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
