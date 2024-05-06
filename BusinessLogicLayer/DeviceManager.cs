using DataTransferObjects;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer;

public class DeviceManager(IServiceProvider serviceProvider) : Manager(serviceProvider)
{

  // TODO: Nullable
  public async Task<DocumentDto?> GetDeviceById(int id)
  {
    var model = await Context.Set<Document>().FindAsync(id);
    return Mapper.Map<DocumentDto>(model);
  }

  public async Task<IEnumerable<DocumentListDto>> GetDevicesAsync()
  {
    var models = await Context.Set<Document>().ToListAsync();
    return Mapper.Map<IEnumerable<DocumentListDto>>(models);
  }

  public async Task<IEnumerable<DocumentListDto>> GetDevicesByNameAsync(string name, bool sort)
  {
    var query = Context.Set<Document>()
      .Where(e => e.Name == name);
      
    if (sort)
    {
      query = query.OrderBy(e => e.Name);
    } else
    {
      query = query.OrderByDescending(e => e.Name); 
    }
    var models = await query.AsNoTracking().ToListAsync();
    return Mapper.Map<IEnumerable<DocumentListDto>>(models);
  }

  // TODO: Use Dto for return type
  public async Task<DataResult<Document>> AddOrUpdateDeviceAsync(DocumentDto deviceDto)
  {
    var model = Mapper.Map<Document>(deviceDto);
    Context.Entry(model).State = model.Id == default ? EntityState.Added : EntityState.Modified;

    try
    {
      await Context.SaveChangesAsync();

      return new DataResult<Document>(model, true, null);
    }
    catch (DbUpdateConcurrencyException ex) // when (ex.Data.)
    {
      return new DataResult<Document>(null!, false, ex);
    }
    catch (DbUpdateException ex)
    {
      return new DataResult<Document>(null!, false, ex);
    }
  }

  public async Task DeleteDeviceAsync(Document device)
  {
    Context.Set<Document>().Remove(device);
    await Context.SaveChangesAsync();
  }
}
