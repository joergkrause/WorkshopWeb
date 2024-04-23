using DataAccessLayer;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer;

public class DeviceManager(DevicesContext context) : Manager(context)
{

  // TODO: Nullable
    public async Task<Device?> GetDeviceById(int id)
  {
        return await Context.Set<Device>().FindAsync(id);
    }

  //  public async Task<List<Device>> GetDevicesAsync()
  //{
  //      return await Context.Devices.ToListAsync();
  //  }

  //  public async Task AddDeviceAsync(Device device)
  //{
  //      Context.Devices.Add(device);
  //      await Context.SaveChangesAsync();
  //  }

  //  public async Task UpdateDeviceAsync(Device device)
  //{
  //      Context.Devices.Update(device);
  //      await Context.SaveChangesAsync();
  //  }

  //  public async Task DeleteDeviceAsync(Device device)
  //{
  //      Context.Devices.Remove(device);
  //      await Context.SaveChangesAsync();
  //  }
}
