using Driver.Common.DTO.Driver;
using System.Collections.Generic;

namespace Driver.Application.Services.Driver
{
    public interface IRandomDriverService
    {
        List<AddDriverDto> GenerateRandomDrivers(int count);

        string Alphabetize(string fullName);
    }
}
