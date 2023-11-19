using Driver.Common.DTO.Driver;


// ReSharper disable once CheckNamespace
namespace Driver.Application.Mapping
{
    public partial class MappingService
    {
        public void MapDriver()
        {
            CreateMap<Driver.Domain.Entities.Driver, DriverDto>()
                .ReverseMap();

            CreateMap<Driver.Domain.Entities.Driver, AddDriverDto>()
                .ReverseMap();

            CreateMap<Driver.Domain.Entities.Driver, UpdateDriverDto>()
                .ReverseMap();
        }
    }
}