using AutoMapper;
using DataTransferObjects;
using DomainModels;

namespace BusinessLogicLayer.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {

      CreateMap<Document, DocumentDto>()
        .ForMember(e => e.HasContent, opt => opt.MapFrom(e => e.Content != null))
        .ReverseMap();

      CreateMap<Document, DocumentListDto>();
      CreateMap<Document, DocumentDetailsDto>();

      CreateMap<CreateDocumentDto, Document>();
      CreateMap<UpdateDocumentDto, Document>();

      CreateMap<Content, ContentDto>().ReverseMap();

    }
  }
}
