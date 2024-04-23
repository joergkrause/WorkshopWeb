using DataAccessLayer;

namespace BusinessLogicLayer;

public abstract class Manager(DevicesContext context)
{
  protected DevicesContext Context => context;
}