using AutoFixture;
using AutoMapper;
using Driver.Application.Services.Driver;
using Driver.Common.Abstraction.UnitOfWork;
using Driver.Common.DTO.Driver;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace Driver.Application.Unit.Tests.Service
{
    public class DriverServiceTests : AutoFixtureBase
    {
        private readonly Mock<IUnitOfWork<Domain.Entities.Driver>> _uowMock;
        private readonly DriverService _driverServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        public DriverServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork<Domain.Entities.Driver>>();
            Fixture.Register(() => _uowMock.Object);

            _mapperMock = new Mock<IMapper>();
            Fixture.Register(() => _mapperMock.Object);

            _driverServiceMock = new DriverService(_uowMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList()
        {
            // arrange
            var entities = Fixture.Build<Domain.Entities.Driver>().CreateMany();
            var mapped = Fixture.Build<DriverDto>().CreateMany().ToList();

            _uowMock.Setup(x => x.Repository.
                GetAllAsync(It.IsAny<Func<IQueryable<Domain.Entities.Driver>, IIncludableQueryable<Domain.Entities.Driver, object>>>()
                , It.IsAny<bool>())).Returns(Task.FromResult(entities));

            _mapperMock.Setup(x => x.Map<IEnumerable<Domain.Entities.Driver>, List<DriverDto>>(It.IsAny<IEnumerable<Domain.Entities.Driver>>()))
                .Returns(mapped);

            // act
            var result = await _driverServiceMock.GetAllAsync();

            // assert
            Assert.True(result.Any());
        }
    }
}
