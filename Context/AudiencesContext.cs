using YP02.Models;
using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;

namespace YP02.Context
{
    public class AudiencesContext : DbContext
    {
        public DbSet<Audiences> Audiences { get; set; }
        public AudiencesContext()
        {
            Database.EnsureCreated();
            Audiences.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
