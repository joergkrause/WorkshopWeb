using AutoMapper;
using DataTransferObjects;
using DomainModels;

namespace BusinessLogicLayer.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Document, DeviceDto>()
        .ForMember(e => e.HasValues, opt => opt.MapFrom(e => e.Values.Count > 0))
        .ReverseMap();

      CreateMap<Document, DeviceListDto>();
      CreateMap<Document, DeviceDetailsDto>();

      CreateMap<CreateDeviceDto, Document>();
      CreateMap<UpdateDeviceDto, Document>();

      CreateMap<Category, MeasureValueDto>().ReverseMap();

    }
  }
}
