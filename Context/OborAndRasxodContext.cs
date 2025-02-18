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
    public class OborAndRasxodContext : DbContext
    {
        public DbSet<OborAndRasxod> OborAndRasxod { get; set; }
        public OborAndRasxodContext()
        {
            Database.EnsureCreated();
            OborAndRasxod.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
