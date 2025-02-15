using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class MaterialsContext : DbContext
    {
        public DbSet<Materials> Materials { get; set; }
        public MaterialsContext()
        {
            Database.EnsureCreated();
            Materials.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
