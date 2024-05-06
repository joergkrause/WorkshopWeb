using System.Numerics;

namespace DomainModels;

public class Category : EntityBase
{
  public bool IsActive { get; set; }

  public string Name { get; set; } = default!;

  public ICollection<Document> Documents { get; set; } = [];
}