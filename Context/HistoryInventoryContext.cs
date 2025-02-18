using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class HistoryInventoryContext : DbContext
    {
        public DbSet<HistoryInventory> HistoryInventory { get; set; }
        public HistoryInventoryContext()
        {
            Database.EnsureCreated();
            HistoryInventory.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
