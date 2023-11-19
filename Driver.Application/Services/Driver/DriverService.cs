using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Driver.Common.Abstraction.UnitOfWork;
using Driver.Common.DTO.Driver;

namespace Driver.Application.Services.Driver
{
    public class DriverService : IDriverService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<Domain.Entities.Driver> _uow;
        public DriverService(IUnitOfWork<Domain.Entities.Driver> uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<DriverDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        
        public async Task<List<DriverDto>> GetAllAsync()
        {
            var entities = await _uow.Repository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<Domain.Entities.Driver>, List<DriverDto>>(entities);
            return data;
        }

 

        public async Task<DriverDto> AddAsync(AddDriverDto model)
        {
            throw new NotImplementedException();
        }

      

        public async Task<DriverDto> UpdateAsync(UpdateDriverDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
