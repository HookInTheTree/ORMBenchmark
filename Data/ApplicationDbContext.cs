using FactoryMethod.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Data
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString { get; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        public DbSet<Person> person { get; set; }
        public DbSet<Person2> person2 { get; set; }

    }
}
