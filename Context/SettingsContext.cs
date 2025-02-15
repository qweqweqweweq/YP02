using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class SettingsContext : DbContext
    {
        public DbSet<Settings> Settings { get; set; }
        public SettingsContext()
        {
            Database.EnsureCreated();
            Settings.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
