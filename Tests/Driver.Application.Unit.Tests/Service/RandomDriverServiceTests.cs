using Driver.Application.Services.Driver;

namespace Driver.Application.Unit.Tests.Service
{
    public class RandomDriverServiceTests : AutoFixtureBase
    {

        [Fact]
        public void GenerateRandomDrivers_ShouldReturnListOfSpecifiedCount()
        {
            // Arrange
            var randomDriverService = new RandomDriverService();
            var count = 5; 

            // Act
            var result = randomDriverService.GenerateRandomDrivers(count);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(count, result.Count);
        }

        [Fact]
        public void GenerateRandomDrivers_ShouldGenerateRandomNames()
        {
            // Arrange
            var randomDriverService = new RandomDriverService();
            var count = 5;

            // Act
            var result = randomDriverService.GenerateRandomDrivers(count);

            // Assert
            Assert.All(result, driver =>
            {
                Assert.NotNull(driver.FirstName);
                Assert.NotNull(driver.LastName);
                Assert.NotEqual(driver.FirstName, driver.LastName);
            });
        }

        [Fact]
        public void GenerateRandomDrivers_ShouldGenerateRandomEmails()
        {
            // Arrange
            var randomDriverService = new RandomDriverService();
            var count = 5; 

            // Act
            var result = randomDriverService.GenerateRandomDrivers(count);

            // Assert
            Assert.All(result, driver =>
            {
                Assert.NotNull(driver.Email);
                Assert.Contains("@", driver.Email);
            });
        }

        [Fact]
        public void GenerateRandomDrivers_ShouldGenerateRandomPhoneNumbers()
        {
            // Arrange
            var randomDriverService = new RandomDriverService();
            var count = 5; 

            // Act
            var result = randomDriverService.GenerateRandomDrivers(count);

            // Assert
            Assert.All(result, driver =>
            {
                Assert.NotNull(driver.PhoneNumber);
                Assert.Contains("-", driver.PhoneNumber);
            });
        }

        [Theory]
        [InlineData("Mahmoud Ragab", "Madhmou Raabg")]
        [InlineData("Ahmed Gafar", "Adehm Gaafr")]
        [InlineData("Hossam Alaskalany", "Hamoss Aaaakllnsy")]
        public void Alphabetize_ShouldReturnAlphabetizedName_WhenFullNameProvided(string fullName, string expectedAlphabetizedName)
        {
            // Arrange
            var randomDriverService = new RandomDriverService();

            // Act
            var result = randomDriverService.Alphabetize(fullName);

            // Assert
            Assert.Equal(expectedAlphabetizedName, result);
        }



        [Fact]
        public void Alphabetize_ShouldReturnEmptyString_WhenEmptyFullNameProvided()
        {
            // Arrange
            var randomDriverService = new RandomDriverService();

            // Act
            var result = randomDriverService.Alphabetize(string.Empty);

            // Assert
            Assert.Equal(string.Empty, result);
        }

    }
}
