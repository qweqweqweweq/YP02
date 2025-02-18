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
    public class ResponUserAndOborContext : DbContext
    {
        public DbSet<ResponUserAndObor> ResponUserAndObor { get; set; }
        public ResponUserAndOborContext()
        {
            Database.EnsureCreated();
            ResponUserAndObor.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}
