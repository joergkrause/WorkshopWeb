using AutoMapper;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public abstract class Manager(IServiceProvider serviceProvider)
{
  protected DevicesContext Context => serviceProvider.GetRequiredService<DevicesContext>();

  protected IMapper Mapper => serviceProvider.GetRequiredService<IMapper>();
}