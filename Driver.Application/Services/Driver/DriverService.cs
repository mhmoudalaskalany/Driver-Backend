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
            var entity = await _uow.Repository.GetAsync(id);
            var mapped = _mapper.Map<DriverDto>(entity);
            return mapped;
        }

        
        public async Task<List<DriverDto>> GetAllAsync()
        {
            var entities = await _uow.Repository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<Domain.Entities.Driver>, List<DriverDto>>(entities);
            return data;
        }

 

        public async Task<DriverDto> AddAsync(AddDriverDto model)
        {
            var entity = _mapper.Map<AddDriverDto, Domain.Entities.Driver>(model);
            var result = await _uow.Repository.AddAsync(entity);
            var mapped = _mapper.Map<DriverDto>(result);
            return mapped;
        }

      

        public async Task<DriverDto> UpdateAsync(UpdateDriverDto model)
        {
            var entity = _mapper.Map<UpdateDriverDto, Domain.Entities.Driver>(model);
            var result = await _uow.Repository.UpdateAsync(entity);
            var mapped = _mapper.Map<DriverDto>(result);
            return mapped;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _uow.Repository.GetAsync(id);
            var result = await _uow.Repository.DeleteAsync(entity);
            return result;
        }
    }
}
