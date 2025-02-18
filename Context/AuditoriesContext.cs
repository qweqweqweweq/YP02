using YP02.Models;
using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;

namespace YP02.Context
{
    public class AudiencesContext : DbContext
    {
        public DbSet<Auditories> Auditories { get; set; }
        public AudiencesContext()
        {
            Database.EnsureCreated();
            Auditories.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
