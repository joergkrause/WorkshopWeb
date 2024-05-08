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

public class DocumentContext(DbContextOptions<DocumentContext> options, IUserContext userContext) : DbContext(options)
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

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var entries = ChangeTracker.Entries<IEntityBase>();
    foreach (var entry in entries)
    {
      switch (entry.State)
      {
        case EntityState.Added:
          entry.Property<string>("Created").CurrentValue = userContext.Principal.Identity.Name; 
          entry.Property<string>("Modified").CurrentValue = userContext.Principal.Identity.Name;
          break;
        case EntityState.Modified:
          entry.Property<string>("Modified").CurrentValue = userContext.Principal.Identity.Name;
          break;
      }
    }
    return base.SaveChangesAsync(cancellationToken);
  }

  private Expression<Func<TEntity,bool>> SoftDeleteFilter<TEntity>() where TEntity : IEntityBase
  {
    return e => EF.Property<bool>(e, nameof(ISoftDeleteProperties.IsDeleted)) == false;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Document>().ToTable("Documents");
    modelBuilder.Entity<Document>().HasKey(e => e.Id);
    modelBuilder.Entity<Document>().Property(e => e.Name).HasMaxLength(20).IsRequired();
    modelBuilder.Entity<Document>().Property(e => e.Description).HasMaxLength(200);
    modelBuilder.Entity<Document>().Property<bool>(nameof(ISoftDeleteProperties.IsDeleted));
    modelBuilder.Entity<Document>().HasQueryFilter(SoftDeleteFilter<Document>());
    modelBuilder.Entity<Document>().Property<string>("Created");  
    modelBuilder.Entity<Document>().Property<string>("Modified");
    modelBuilder.Entity<Document>()
      .HasOne(e => e.Category)
      .WithMany(e => e.Documents)
      .HasForeignKey("CategoryId");

    modelBuilder.Entity<Category>().ToTable("Categories");
    modelBuilder.Entity<Category>().HasKey(e => e.Id);
    modelBuilder.Entity<Category>().Property(e => e.Name).HasMaxLength(10).IsRequired();


    modelBuilder.Entity<Content>().ToTable("Content");
    modelBuilder.Entity<Content>().HasKey(e => e.Id);
    modelBuilder.Entity<Content>().Property(e => e.Text).IsRequired();
  }

 
}

public class MigrationContext : IDesignTimeDbContextFactory<DocumentContext>
{
  public DocumentContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<DocumentContext>();
    optionsBuilder.UseSqlServer("Server=(localdb)\\Workshop;Database=DocumentDb;Trusted_Connection=True;MultipleActiveResultSets=true");

    return new DocumentContext(optionsBuilder.Options);
  }
}
