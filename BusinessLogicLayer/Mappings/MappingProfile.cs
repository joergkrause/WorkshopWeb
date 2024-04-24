using AutoMapper;
using DataTransferObjects;
using DomainModels;

namespace BusinessLogicLayer.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Device, DeviceDto>()
        .ForMember(e => e.HasValues, opt => opt.MapFrom(e => e.Values.Count > 0))
        .ReverseMap();

      CreateMap<Device, DeviceListDto>();
      CreateMap<Device, DeviceDetailsDto>();

      CreateMap<CreateDeviceDto, Device>();
      CreateMap<UpdateDeviceDto, Device>();

      CreateMap<MeasureValue, MeasureValueDto>().ReverseMap();

    }
  }
}
