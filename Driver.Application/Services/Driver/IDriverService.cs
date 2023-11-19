using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Driver.Common.DTO.Driver;

namespace Driver.Application.Services.Driver
{
    public interface IDriverService
    {
        Task<DriverDto> GetAsync();

        Task<List<DriverDto>> GetAllAsync();

        Task<List<DriverDto>> GetPagedAsync();

        Task<DriverDto> AddAsync();

        Task<DriverDto> UpdateAsync();

        Task<Guid> DeleteAsync();
    }
}
