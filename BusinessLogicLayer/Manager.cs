﻿using AutoMapper;
using DataAccessLayer;
using DomainModels;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public abstract class Manager(IServiceProvider serviceProvider)
{
  protected DocumentContext Context => serviceProvider.GetRequiredService<DocumentContext>();

  protected IMapper Mapper => serviceProvider.GetRequiredService<IMapper>();

  protected IUserContext UserContext => serviceProvider.GetRequiredService<IUserContext>();


}