﻿using System.Data;
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
            var id = 1;
            var expectedDriver = CreateDriver(id);

            mockDbConnection.SetupDapperAsync(c =>
                    c.QueryFirstOrDefaultAsync<Domain.Entities.Driver>(It.IsAny<string>(),
                        It.IsAny<object>(), null, null, null))
                .ReturnsAsync(expectedDriver);

            //Act
            var result = await repository.GetAsync(id);


            //Assert

            Assert.Equal(expectedDriver.Id, result.Id);

        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfEntities()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            var repository = new Repository<Domain.Entities.Driver>(mockDbConnection.Object);
            var expectedResult = new List<Domain.Entities.Driver>
        {
            new() { Id = 1, FirstName = "Mahmoud", LastName = "Ragab" },
            new() { Id = 2, FirstName = "Ahmed", LastName = "Ragab" }
        };

            mockDbConnection.SetupDapperAsync(c => c.QueryAsync<Domain.Entities.Driver>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
                          .ReturnsAsync(expectedResult);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(expectedResult.Count, result.Count());
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntityToDatabase()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            var repository = new Repository<Domain.Entities.Driver>(mockDbConnection.Object);
            var entityToAdd = CreateDriver(1);

            mockDbConnection
                .SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);
            // Act
            var result = await repository.AddAsync(entityToAdd);

            // Assert
            Assert.Equal(entityToAdd.Id , result.Id);
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddEntityToDatabaseAndReturnCount()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            var repository = new Repository<Domain.Entities.Driver>(mockDbConnection.Object);
            var driver1 = CreateDriver(1);
            var driver2 = CreateDriver(2);
            var entities  = new List<Domain.Entities.Driver> { driver1, driver2 };
            mockDbConnection
                .SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<List<Domain.Entities.Driver>>(), null, null, null))
                .ReturnsAsync(1);
            // Act
            var result = await repository.AddRangeAsync(entities);

            // Assert
            Assert.Equal(entities.Count, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntityInDatabase()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            var repository = new Repository<Domain.Entities.Driver>(mockDbConnection.Object);
            var entityToUpdate = CreateDriver(1);

            mockDbConnection
                .SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);
            // Act
            var result =await repository.UpdateAsync(entityToUpdate);

            // Assert
            Assert.Equal(entityToUpdate.Id, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntityFromDatabase()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            var repository = new Repository<Domain.Entities.Driver>(mockDbConnection.Object);
            var entityToDelete = CreateDriver(1);

            mockDbConnection
                .SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            // Act
            await repository.AddAsync(entityToDelete);
            var deleted = await repository.DeleteAsync(entityToDelete);

            // Assert
            Assert.True(deleted);
        }


        [Fact]
        public void CreateDriversTable_ShouldCreateOrUpdateTable()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            var repository = new Repository<Domain.Entities.Driver>(mockDbConnection.Object);
       

            mockDbConnection
                .SetupDapper(c => c.Execute(It.IsAny<string>(), null, null, null, null))
                .Returns(1);
            // Act
            var result =  repository.CreateDriversTable();

            // Assert
            Assert.Equal(1, result);
        }



        private Domain.Entities.Driver CreateDriver(int id)
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
