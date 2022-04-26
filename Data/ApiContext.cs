using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wsr.Models;

namespace Wsr.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Wsr");
        }

        /* Seed method 26.04.2022 */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User("Zbyszek", "p2i35nhjp1ip", true),
                new User("Marcel", "p2i35nhjp1ip", false)
                );
        }
    }
}