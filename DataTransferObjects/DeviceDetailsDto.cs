using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
  public class DeviceDetailsDto
  {
    public string Name { get; set; } = default!;

    public ICollection<MeasureValueDto> Values { get; set; } = [];
  }
}
