using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels;

public class Document : EntityBase, ISoftDelete
{
  public string Name { get; set; } = default!;

  public string? Description { get; set; }

  public ICollection<Category> Categories { get; set; } = [];

  public ICollection<Content> Content { get; set; } = [];
}
