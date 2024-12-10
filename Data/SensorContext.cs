using CapteurManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapteurManagement.Data
{
    public class SensorContext:DbContext
    {
        public SensorContext(DbContextOptions<SensorContext> options) : base(options)
        {
        }

        // DbSet for Sensors
        public DbSet<Sensor> Sensors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the table name and any constraints (optional)
            modelBuilder.Entity<Sensor>().ToTable("Sensors");
        }
    }
}
