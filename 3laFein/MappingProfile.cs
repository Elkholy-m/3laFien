using AutoMapper;
using Entities.Models;
using Shared.DTO;

namespace _3laFein
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // This Is Configuration For Auto Mapping Package
            CreateMap<Visitor, VisitorDto>();
            CreateMap<VisitorForCreationDto, Visitor>();
            CreateMap<VisitorForUpdateDto, Visitor>();

            CreateMap<SocialAccount, SocialAccountDto>();
            CreateMap<SocialAccountForCreationDto, SocialAccount>();
            CreateMap<SocialAccountForUpdateDto, SocialAccount>();
        }
    }
}
