using Microsoft.EntityFrameworkCore;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class TypeModelContext : DbContext
    {
        public DbSet<TypeModel> TypeModel { get; set; }
        public TypeModelContext()
        {
            Database.EnsureCreated();
            TypeModel.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
