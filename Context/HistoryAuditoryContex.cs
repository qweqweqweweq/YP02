using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class HistoryAuditoryContex : DbContext
    {
        public DbSet<HistoryAuditory> HistoryAuditory { get; set; }
        public HistoryAuditoryContex()
        {
            Database.EnsureCreated();
            HistoryAuditory.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
