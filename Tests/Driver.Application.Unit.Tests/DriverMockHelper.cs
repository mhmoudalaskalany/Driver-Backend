﻿using Driver.Common.DTO.Driver;

namespace Driver.Application.Unit.Tests
{
    public static class DriverMockHelper
    {
        public static AddDriverDto CreateAddDriverDto()
        {
            var driver = new AddDriverDto()
            {
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }

        public static List<AddDriverDto> CreateDriversDtoList()
        {
            var drivers = new List<AddDriverDto>();
            for (var i = 0; i < 10; i++)
            {
                drivers.Add(CreateAddDriverDto());
            }

            return drivers;
        }

        public static List<Domain.Entities.Driver> CreateDriversList(List<AddDriverDto> driversDto)
        {
            var drivers = new List<Domain.Entities.Driver>();
            var count = 1;
            foreach (var driverDto in driversDto)
            {
                drivers.Add( new Domain.Entities.Driver()
                {
                    Email = driverDto.Email,
                    FirstName = driverDto.FirstName,
                    LastName = driverDto.LastName,
                    PhoneNumber = driverDto.PhoneNumber,
                    Id = count++
                });
            }

            return drivers;
        }


        public static UpdateDriverDto CreateUpdateDriverDto(int id)
        {
            var driver = new UpdateDriverDto()
            {
                Id = id,
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }

        public static DriverDto CreateDriverDto(int id)
        {
            var driver = new DriverDto()
            {
                Id = id,
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }


        public static Domain.Entities.Driver CreateDriver()
        {
            var driver = new Domain.Entities.Driver()
            {
                Id = 1,
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }
    }
}
