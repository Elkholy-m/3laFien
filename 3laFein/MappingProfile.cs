using AutoMapper;
using Entities.Models;
using NetTopologySuite.Geometries;
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

            CreateMap<PlaceImage, PlaceImageDto>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewForCreationDto, Review>();
            CreateMap<ReviewForUpdateDto, Review>();

            // 1. Entity -> Read DTO
            CreateMap<Place, PlaceDto>()
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location.X))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location.Y));
            // You NO LONGER need the math logic here for DiscountedPrice.
            // AutoMapper automatically maps Place.DiscountedPrice -> PlaceDto.DiscountedPrice

            // 2. Manipulation DTO -> Entity
            CreateMap<PlaceForUpdateDto, Place>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src =>
                    (src.Latitude.HasValue && src.Longitude.HasValue)
                    ? CreatePoint(src.Latitude.Value, src.Longitude.Value)
                    : null))
                .ForMember(dest => dest.PlaceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<PlaceForCreationDto, Place>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src =>
                    (src.Latitude.HasValue && src.Longitude.HasValue)
                    ? CreatePoint(src.Latitude.Value, src.Longitude.Value)
                    : null))
                .ForMember(dest => dest.PlaceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<PlaceSchedule, PlaceScheduleDto>();
            CreateMap<PlaceScheduleForCreationDto, PlaceSchedule>();
            CreateMap<PlaceScheduleForUpdateDto, PlaceSchedule>();
        }

        // Helper method to create the NTS Point
        private Point CreatePoint(double lat, double lon)
            {
                // SRID 4326 is standard for GPS (WGS 84)
                var geometryFactory = new GeometryFactory(new PrecisionModel(), 4326);

                // IMPORTANT: The order is (Longitude, Latitude) -> (X, Y)
                return geometryFactory.CreatePoint(new Coordinate(lon, lat));
            }
    }
}
