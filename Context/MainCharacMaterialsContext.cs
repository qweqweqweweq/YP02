using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class MainCharacMaterialsContext : DbContext
    {
        public DbSet<MainCharacMaterials> MainCharacMaterials { get; set; }
        public MainCharacMaterialsContext()
        {
            Database.EnsureCreated();
            MainCharacMaterials.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
