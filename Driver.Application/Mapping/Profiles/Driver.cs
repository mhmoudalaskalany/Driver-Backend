using Driver.Common.DTO.Driver;


// ReSharper disable once CheckNamespace
namespace Driver.Application.Mapping
{
    public partial class MappingService
    {
        public void MapDriver()
        {
            CreateMap<Domain.Entities.Driver, DriverDto>()
                .ReverseMap();

            CreateMap<AddDriverDto, Domain.Entities.Driver>()
                .ForMember(dest => dest.Id , opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Domain.Entities.Driver, UpdateDriverDto>()
                .ReverseMap();
        }
    }
}