using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
  }

  public override int SaveChanges()
  {
    throw new InvalidOperationException("no, async only");
  }

  private Expression<Func<TEntity,bool>> SoftDeleteFilter<TEntity>() where TEntity : IEntityBase
  {
    return e => EF.Property<bool>(e, nameof(ISoftDeleteProperties.IsDeleted)) == false;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Document>().ToTable("Devices");
    modelBuilder.Entity<Document>().HasKey(e => e.Id);
    modelBuilder.Entity<Document>().Property(e => e.Name).HasMaxLength(20).IsRequired();
    modelBuilder.Entity<Document>().Property(e => e.Description).HasMaxLength(200);
    modelBuilder.Entity<Document>().Property<bool>(nameof(ISoftDeleteProperties.IsDeleted));
    modelBuilder.Entity<Document>().HasQueryFilter(SoftDeleteFilter<Document>());
    modelBuilder.Entity<Document>()
      .HasMany(e => e.Categories)
      .WithOne()
      .HasForeignKey("DocumentId");

    modelBuilder.Entity<Category>().ToTable("Categories");
    modelBuilder.Entity<Category>().HasKey(e => e.Id);
    modelBuilder.Entity<Category>().Property(e => e.Name).HasMaxLength(10).IsRequired();

    modelBuilder.Entity<Content>().ToTable("Content");
    modelBuilder.Entity<Content>().HasKey(e => e.Id);
    modelBuilder.Entity<Content>().Property(e => e.Text).IsRequired();
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
