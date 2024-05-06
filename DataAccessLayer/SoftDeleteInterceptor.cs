using DomainModels;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccessLayer;

internal class SoftDeleteInterceptor : SaveChangesInterceptor
{
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
  {
    if (eventData.Context is not null && eventData.Context.ChangeTracker is not null and ChangeTracker ct) 
    {
      var entries = ct.Entries<ISoftDelete>();
      foreach (var entry in entries)
      {
        if (entry.State == EntityState.Deleted)
        {
          entry.State = EntityState.Unchanged;
          entry.Property<bool>(nameof(ISoftDeleteProperties.IsDeleted)).CurrentValue = true;
          entry.Property<bool>(nameof(ISoftDeleteProperties.IsDeleted)).IsModified = true;
        }
      }
    }
    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }
}