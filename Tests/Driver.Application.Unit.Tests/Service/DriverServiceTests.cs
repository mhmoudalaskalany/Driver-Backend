using AutoFixture;
using AutoFixture.Idioms;
using AutoMapper;
using Driver.Application.Mapping;
using Driver.Application.Services.Driver;
using Driver.Common.Abstraction.Repository;
using Driver.Common.DTO.Driver;
using Moq;

namespace Driver.Application.Unit.Tests.Service
{
    public class DriverServiceTests : AutoFixtureBase
    {
        private readonly Mock<IRepository<Domain.Entities.Driver>> _repositoryMock;
        private readonly MapperConfiguration _mapperConfig;
        public DriverServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Domain.Entities.Driver>>();
            Fixture.Register(() => _repositoryMock.Object);

            _mapperConfig = new MapperConfiguration(config => config.AddProfile<MappingService>());
            Fixture.Register(() => _mapperConfig.CreateMapper());

        }

        [Fact]
        public void Check_Dependencies_Guard_Clause()
        {
            var assertion = new GuardClauseAssertion(Fixture);
            assertion.Verify(typeof(DriverService).GetConstructors());
        }

        [Fact]
        public void Check_AutoMapper()
        {
            _mapperConfig.AssertConfigurationIsValid();
        }

        [Fact]
        public async Task GeAsync_ReturnItem()
        {
            // arrange
            var entity = Fixture.Build<Domain.Entities.Driver>().Create();
            var mapped = Fixture.Build<DriverDto>().Create();

            _repositoryMock.Setup(x => x.
                GetAsync(entity.Id)).Returns(Task.FromResult(entity));

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<Domain.Entities.Driver, DriverDto>(It.IsAny<Domain.Entities.Driver>()))
                .Returns(mapped);
            var driverService = Fixture.Create<DriverService>();

            // act
            var result = await driverService.GetAsync(entity.Id);

            // assert
            Assert.Equal(result.Id , entity.Id);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList()
        {
            // arrange
            var entities = Fixture.Build<Domain.Entities.Driver>().CreateMany();
            var mapped = Fixture.Build<DriverDto>().CreateMany().ToList();

            _repositoryMock.Setup(x => x.
                GetAllAsync()).Returns(Task.FromResult(entities));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<Domain.Entities.Driver>, List<DriverDto>>(It.IsAny<IEnumerable<Domain.Entities.Driver>>()))
                .Returns(mapped);
            var driverService = Fixture.Create<DriverService>();
            // act
            var result = await driverService.GetAllAsync();

            // assert
            Assert.True(result.Any());
        }
    }
}
