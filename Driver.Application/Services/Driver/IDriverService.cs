using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Driver.Common.DTO.Driver;

namespace Driver.Application.Services.Driver
{
    public interface IDriverService
    {
        Task<DriverDto> GetAsync(Guid id);

        Task<List<DriverDto>> GetAllAsync();

        Task<DriverDto> AddAsync(AddDriverDto model);

        Task<DriverDto> UpdateAsync(UpdateDriverDto model);

        Task<bool> DeleteAsync(Guid id);
    }
}
