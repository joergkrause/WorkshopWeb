using DataTransferObjects;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer;

public class DeviceManager(IServiceProvider serviceProvider) : Manager(serviceProvider)
{

  // TODO: Nullable
  public async Task<DeviceDto?> GetDeviceById(int id)
  {
    var model = await Context.Set<Device>().FindAsync(id);
    return Mapper.Map<DeviceDto>(model);
  }

  public async Task<IEnumerable<DeviceListDto>> GetDevicesAsync()
  {
    var models = await Context.Set<Device>().ToListAsync();
    return Mapper.Map<IEnumerable<DeviceListDto>>(models);
  }

  public async Task<IEnumerable<DeviceListDto>> GetDevicesByNameAsync(string name, bool sort)
  {
    var query = Context.Set<Device>()
      .Where(e => e.Name == name);
      
    if (sort)
    {
      query = query.OrderBy(e => e.Name);
    } else
    {
      query = query.OrderByDescending(e => e.Name); 
    }
    var models = await query.AsNoTracking().ToListAsync();
    return Mapper.Map<IEnumerable<DeviceListDto>>(models);
  }

  // TODO: Use Dto for return type
  public async Task<DataResult<Device>> AddOrUpdateDeviceAsync(DeviceDto deviceDto)
  {
    var model = Mapper.Map<Device>(deviceDto);
    Context.Entry(model).State = model.Id == default ? EntityState.Added : EntityState.Modified;

    try
    {
      await Context.SaveChangesAsync();

      return new DataResult<Device>(model, true, null);
    }
    catch (DbUpdateConcurrencyException ex) // when (ex.Data.)
    {
      return new DataResult<Device>(null!, false, ex);
    }
    catch (DbUpdateException ex)
    {
      return new DataResult<Device>(null!, false, ex);
    }
  }

  public async Task DeleteDeviceAsync(Device device)
  {
    Context.Set<Device>().Remove(device);
    await Context.SaveChangesAsync();
  }
}
