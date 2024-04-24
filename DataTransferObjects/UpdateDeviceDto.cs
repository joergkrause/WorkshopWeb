﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
  public class UpdateDeviceDto
  {

    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

  }
}
