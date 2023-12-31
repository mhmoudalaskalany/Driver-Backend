﻿using System;
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

            var randomDrivers = Enumerable.Range(1, count)
                .Select(_ => new AddDriverDto
                {
                    FirstName = GenerateRandomString(random, 5), 
                    LastName = GenerateRandomString(random, 5), 
                    Email = $"{Guid.NewGuid()}@example.com",
                    PhoneNumber = GenerateRandomPhoneNumber(random)
                })
                .ToList();

            return randomDrivers;
        }

        public string Alphabetize(string fullName)
        {
            var words = fullName.Split(' ');
            
            var alphabetizedWords = words.Select(AlphabetizeWord);
            
            var alphabetizedName = string.Join(' ', alphabetizedWords);

            return alphabetizedName;
        }

        private string GenerateRandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateRandomPhoneNumber(Random random)
        {
            return $"{random.Next(100, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
        }

        private string AlphabetizeWord(string word)
        {
            var charArray = word.ToCharArray();
            
            Array.Sort(charArray);
            
            var sortedWord = new string(charArray);

            return sortedWord;
        }
    }
}
