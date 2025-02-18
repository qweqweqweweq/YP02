using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class TypeRasxodContext : DbContext
    {
        public DbSet<TypeRasxod> TypeRasxod { get; set; }
        public TypeRasxodContext()
        {
            Database.EnsureCreated();
            TypeRasxod.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
