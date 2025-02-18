using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class NetworkSettingsContext : DbContext
    {
        public DbSet<NetworkSettings> NetworkSettings { get; set; }
        public NetworkSettingsContext()
        {
            Database.EnsureCreated();
            NetworkSettings.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
