using System;
using allergenerator.Models;
using Microsoft.EntityFrameworkCore;

namespace allergenerator.Data
{
    public class FoodDbContext : DbContext
    {
        
            public DbSet<Food> Foods { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Sensitivity> Sensitivities { get; set; }
            public DbSet<FoodSensitivity> FoodSensitivities { get; set; }

        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<FoodSensitivity>()
                    .HasKey(j => new { j.FoodId, j.SensitivityId });
            }
        }
    }
