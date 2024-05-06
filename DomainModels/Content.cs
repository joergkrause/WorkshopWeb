using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels;

public class Content : EntityBase
{
  public string Text { get; set; }

  public int Version { get; set; }

  public Document Document { get; set; }

  public DateTime Created { get; set; }
}
