using System;
using System.Collections.Generic;
using System.Linq;
using Driver.Common.DTO.Driver;

namespace Driver.Application.Services.Driver
{
    public class RandomDriverService : IRandomDriverService
    {
        public List<AddDriverDto> GenerateRandomDrivers(int count)
        {
            var random = new Random();
            var names = new List<string>
            {
                "user1", "user2", "user3", "user4", "user5", "user6", "user7", "user8", "user9", "user10"
            };

            var randomDrivers = Enumerable.Range(1, count)
                .Select(_ => new AddDriverDto
                {
                    FirstName = names[random.Next(names.Count)],
                    LastName = names[random.Next(names.Count)],
                    Email = $"{Guid.NewGuid()}@gmail.com",
                    PhoneNumber = GenerateRandomPhoneNumber()
                })
                .ToList();

            return randomDrivers;
        }

        private string GenerateRandomPhoneNumber()
        {
            var random = new Random();
            return $"{random.Next(100, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
        }
    }
}
