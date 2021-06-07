using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketManagement.Model
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<VendorEntity> Vendors { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {

        }

        // Here we could modify connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("connection string");
            base.OnConfiguring(optionsBuilder);
        }

        // Here we could modify model entity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<OrganizationEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<VendorEntity>().HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
