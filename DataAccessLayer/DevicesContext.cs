using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer;

public class DevicesContext(DbContextOptions<DevicesContext> options) : DbContext(options)
{

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
#if DEBUG
    optionsBuilder.LogTo(Console.WriteLine);
    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();
#endif
  }

  public override int SaveChanges()
  {
    throw new InvalidOperationException("no, async only");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Device>().ToTable("Devices");
    modelBuilder.Entity<Device>().HasKey(e => e.Id);
    modelBuilder.Entity<Device>().Property(e => e.Name).HasMaxLength(20).IsRequired();
    modelBuilder.Entity<Device>().Property(e => e.Description).HasMaxLength(200);
    modelBuilder.Entity<Device>()
      .HasMany(e => e.Values)
      .WithOne()
      .HasForeignKey("DeviceId");

    modelBuilder.Entity<MeasureValue>().ToTable("MeasureValues");
    modelBuilder.Entity<MeasureValue>().HasKey(e => e.Id);
    modelBuilder.Entity<MeasureValue>().Property(e => e.Value).IsRequired();
    modelBuilder.Entity<MeasureValue>().Property(e => e.Unit).HasMaxLength(10).IsRequired();
  }

 
}

public class MigrationContext : IDesignTimeDbContextFactory<DevicesContext>
{
  public DevicesContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<DevicesContext>();
    optionsBuilder.UseSqlServer("Server=(localdb)\\Workshop;Database=DevicesDb;Trusted_Connection=True;MultipleActiveResultSets=true");

    return new DevicesContext(optionsBuilder.Options);
  }
}
