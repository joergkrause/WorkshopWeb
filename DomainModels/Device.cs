using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels;

public class Device : EntityBase
{
  public string Name { get; set; } = default!;

  public string? Description { get; set; }

  public ICollection<MeasureValue> Values { get; set; } = [];
}
