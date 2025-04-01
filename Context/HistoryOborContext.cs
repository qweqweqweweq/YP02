using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class HistoryOborContext : DbContext
    {
        public DbSet<HistoryObor> HistoryObor { get; set; }
        public HistoryOborContext()
        {
            Database.EnsureCreated();
            HistoryObor.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
