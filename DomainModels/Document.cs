using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels;

public class Document : EntityBase, ISoftDelete
{
  public string Name { get; set; } = default!;

  //[Timestamp]
  //public byte[] MyProperty { get; set; }

  [ConcurrencyCheck]
  public int EditorId { get; set; }

  public string? Description { get; set; }

  public Category Category { get; set; }

  public ICollection<Content> Content { get; set; } = [];
}
