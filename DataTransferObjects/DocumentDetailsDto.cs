using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
  public class DocumentDetailsDto
  {
    public string Name { get; set; } = default!;

    public string Description { get; set; }

    public string CategoryName { get; set; }

    public int LatestContentId { get; set; }
  }
}
