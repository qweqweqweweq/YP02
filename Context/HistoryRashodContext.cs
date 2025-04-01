using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class HistoryRashodContext : DbContext
    {
        public DbSet<HistoryRashod> HistoryRashod { get; set; }
        public HistoryRashodContext()
        {
            Database.EnsureCreated();
            HistoryRashod.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
