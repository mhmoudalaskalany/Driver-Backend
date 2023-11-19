using AutoFixture;
using AutoMapper;
using Driver.Application.Services.Driver;
using Driver.Common.Abstraction.Repository;
using Driver.Common.DTO.Driver;
using Moq;

namespace Driver.Application.Unit.Tests.Service
{
    public class DriverServiceTests : AutoFixtureBase
    {
        private readonly Mock<IRepository<Domain.Entities.Driver>> _repositoryMock;
        private readonly DriverService _driverServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        public DriverServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Domain.Entities.Driver>>();
            Fixture.Register(() => _repositoryMock.Object);

            _mapperMock = new Mock<IMapper>();
            Fixture.Register(() => _mapperMock.Object);

            _driverServiceMock = new DriverService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList()
        {
            // arrange
            var entities = Fixture.Build<Domain.Entities.Driver>().CreateMany();
            var mapped = Fixture.Build<DriverDto>().CreateMany().ToList();

            _repositoryMock.Setup(x => x.
                GetAllAsync()).Returns(Task.FromResult(entities));

            _mapperMock.Setup(x => x.Map<IEnumerable<Domain.Entities.Driver>, List<DriverDto>>(It.IsAny<IEnumerable<Domain.Entities.Driver>>()))
                .Returns(mapped);

            // act
            var result = await _driverServiceMock.GetAllAsync();

            // assert
            Assert.True(result.Any());
        }
    }
}
