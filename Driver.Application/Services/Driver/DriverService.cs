using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Driver.Common.Abstraction.Repository;
using Driver.Common.DTO.Driver;
using Driver.Common.Exceptions;

namespace Driver.Application.Services.Driver
{
    public class DriverService : IDriverService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Driver> _repository;
        private readonly IRandomDriverService _randomDriverService;
        public DriverService(IRepository<Domain.Entities.Driver> uow, IMapper mapper, IRandomDriverService randomDriverService)
        {
            _repository = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _randomDriverService = randomDriverService ?? throw new ArgumentNullException(nameof(randomDriverService));
        }


        public async Task<DriverDto> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(id.ToString());
            }
            var mapped = _mapper.Map<DriverDto>(entity);
            return mapped;
        }

        public async Task<string> GetNameAlphabetizedAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(id.ToString());
            }

            var alphabetizedName = _randomDriverService.Alphabetize(entity.FirstName + " " + entity.LastName);
            return alphabetizedName;
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
            var mapped = _mapper.Map<Domain.Entities.Driver, DriverDto>(result);
            return mapped;
        }

        public async Task<int> AddRandomDriversAsync()
        {
            var randomDrivers = _randomDriverService.GenerateRandomDrivers(10);
            var entities = _mapper.Map<List<AddDriverDto>, List<Domain.Entities.Driver>>(randomDrivers);
            var row = await _repository.AddRangeAsync(entities);
            return row;

        }



        public async Task<DriverDto> UpdateAsync(UpdateDriverDto model)
        {
            var entity = _mapper.Map<UpdateDriverDto, Domain.Entities.Driver>(model);
            var result = await _repository.UpdateAsync(entity);
            var mapped = _mapper.Map<Domain.Entities.Driver, DriverDto>(result);
            return mapped;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            var result = await _repository.DeleteAsync(entity);
            return result;
        }
    }
}
