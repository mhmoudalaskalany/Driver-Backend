using AutoMapper;

namespace Driver.Application.Mapping
{
    public partial class MappingService : Profile
    {
        public MappingService()
        {
            MapDriver();
        }
    }
}