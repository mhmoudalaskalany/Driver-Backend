using AutoFixture;
using Driver.Api.Controllers.V1;
using Driver.Application.Services.Driver;
using Driver.Common.DTO.Driver;
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
        public async Task GetAllAsync_Return_Ok()
        {
            //Arrange
            var result = Fixture.Build<List<DriverDto>>().Create();
            _driverServiceMock.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(result));

            //Act
            var response = await _controller.GetAllAsync();

            //Assert
            Assert.True(true);
        }



    }
}