using System.Net;
using AutoFixture;
using Driver.Api.Controllers.V1;
using Driver.Application.Services.Driver;
using Driver.Common.DTO.Driver;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Driver.Api.Unit.Tests.Controllers.V1
{
    public class DriversControllerUnitTest : AutoFixtureBase
    {
        private readonly Mock<IDriverService> _driverServiceMock;
        private readonly DriversController _controller;
        public DriversControllerUnitTest()
        {
            _driverServiceMock = new Mock<IDriverService>();
            Fixture.Register(() => _driverServiceMock.Object);
            _controller = new DriversController(_driverServiceMock.Object);
        }

        [Fact]
        public async Task GetAsync_Return_Ok()
        {
            //Arrange
            var id = Guid.NewGuid();
            var result = Fixture.Build<DriverDto>().With(c => c.Id , id).Create();
            _driverServiceMock.Setup(x => x.GetAsync(id))
                .Returns(Task.FromResult(result));

            //Act
            var response = (OkObjectResult) await _controller.GetAsync(id);

            //Assert
            Assert.Equal(response.StatusCode , 200);
        }


        [Fact]
        public async Task GetAllAsync_Return_Ok()
        {
            //Arrange
            var result = Fixture.Build<DriverDto>().CreateMany(3);

            _driverServiceMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(result.ToList);

            //Act
            var response = (OkObjectResult)await _controller.GetAllAsync();
            var list = (List<DriverDto>)response.Value;
            //Assert
            Assert.Equal(response.StatusCode, 200);
            Assert.Equal(list.Count, 3);
        }



    }
}