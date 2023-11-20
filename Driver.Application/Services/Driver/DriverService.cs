using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Driver.Common.Abstraction.Repository;
using Driver.Common.DTO.Driver;

namespace Driver.Application.Services.Driver
{
    public class DriverService : IDriverService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Driver> _repository;
        public DriverService(IRepository<Domain.Entities.Driver> uow, IMapper mapper)
        {
            _repository = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<DriverDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            var mapped = _mapper.Map<DriverDto>(entity);
            return mapped;
        }

        
        public async Task<List<DriverDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<Domain.Entities.Driver>, List<DriverDto>>(entities);
            return data;
        }

 

        public async Task<DriverDto> AddAsync(AddDriverDto model)
        {
            var entity = _mapper.Map<AddDriverDto, Domain.Entities.Driver>(model);
            var result = await _repository.AddAsync(entity);
            var mapped = _mapper.Map<DriverDto>(result);
            return mapped;
        }

      

        public async Task<DriverDto> UpdateAsync(UpdateDriverDto model)
        {
            var entity = _mapper.Map<UpdateDriverDto, Domain.Entities.Driver>(model);
            var result = await _repository.UpdateAsync(entity);
            var mapped = _mapper.Map<DriverDto>(result);
            return mapped;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            var result = await _repository.DeleteAsync(entity);
            return result;
        }
    }
}
