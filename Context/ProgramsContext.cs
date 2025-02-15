using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class ProgramsContext : DbContext
    {
        public DbSet<Programs> Programs { get; set; }
        public ProgramsContext()
        {
            Database.EnsureCreated();
            Programs.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
