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
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<PoolTable> PoolTables { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Wsr");
        }

        /* Seed method 26.04.2022 */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}