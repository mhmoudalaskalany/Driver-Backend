using System.Data;
using Dapper;
using Driver.Infrastructure.Repository;
using Moq;
using Moq.Dapper;

namespace Driver.Infrastructure.Unit.Tests.Repository
{
    public class RepositoryTests : AutoFixtureBase
    {
        [Fact]
        public async Task GetAsync_ReturnEntity_WhenExist()
        {
            //Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            var repository = new Repository<Domain.Entities.Driver>(mockDbConnection.Object);
            var id = Guid.NewGuid();
            var expectedDriver = CreateDriver(id);

            mockDbConnection.SetupDapperAsync(c =>
                    c.QueryFirstOrDefaultAsync<Domain.Entities.Driver>(It.IsAny<string>(),
                        It.IsAny<object>(), null , null , null))
                .ReturnsAsync(expectedDriver);

            //Act
            var result = await repository.GetAsync(id);


            //Assert

            Assert.Equal(expectedDriver.Id , result.Id);

        }



        private Domain.Entities.Driver CreateDriver(Guid id)
        {
            var driver = new Domain.Entities.Driver()
            {
                Id = id,
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }
    }
}
