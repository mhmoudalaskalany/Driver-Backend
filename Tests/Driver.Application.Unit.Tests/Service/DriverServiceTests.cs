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
        private readonly Mock<IRandomDriverService> _randomDriverServiceMock;
        private readonly MapperConfiguration _mapperConfig;
        public DriverServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Domain.Entities.Driver>>();
            Fixture.Register(() => _repositoryMock.Object);

            _mapperConfig = new MapperConfiguration(config => config.AddProfile<MappingService>());
            Fixture.Register(() => _mapperConfig.CreateMapper());

            _randomDriverServiceMock = new Mock<IRandomDriverService>();
            Fixture.Register(() => _randomDriverServiceMock.Object);

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
            Assert.Equal(result.Id, entity.Id);
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


        [Fact]
        public async Task AddAsync_CreateItem_AndReturn_Dto()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Domain.Entities.Driver>>();
            var mockMapper = new Mock<IMapper>();
            var service = new DriverService(mockRepository.Object, mockMapper.Object , _randomDriverServiceMock.Object);

            var addDriverDto = CreateAddDriverDto();
            var addedEntity = CreateDriver();
            var addedDto = CreateDriverDto(addedEntity.Id);

            mockMapper.Setup(x => x.Map<AddDriverDto ,  Domain.Entities.Driver>(It.IsAny<AddDriverDto>()))
                .Returns(addedEntity);
            
            mockRepository.Setup(repo => repo.AddAsync(addedEntity)).ReturnsAsync(addedEntity);

            mockMapper.Setup(x => x.Map<Domain.Entities.Driver, DriverDto>(It.IsAny<Domain.Entities.Driver>()))
                .Returns(addedDto);

            // Act
            var result = await service.AddAsync(addDriverDto);

            // Assert
            Assert.Equal(addedDto.Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedDriverDto()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Domain.Entities.Driver>>();
            var mockMapper = new Mock<IMapper>();
            var service = new DriverService(mockRepository.Object, mockMapper.Object, _randomDriverServiceMock.Object);

            var updatedEntity = CreateDriver();
            var updateDriverDto = CreateUpdateDriverDto(updatedEntity.Id);
            var updatedDto = CreateDriverDto(updateDriverDto.Id);


            mockMapper.Setup(x => x.Map<UpdateDriverDto, Domain.Entities.Driver>(It.IsAny<UpdateDriverDto>()))
                .Returns(updatedEntity);

            mockRepository.Setup(repo => repo.UpdateAsync(updatedEntity)).ReturnsAsync(updatedEntity);

            mockMapper.Setup(x => x.Map<Domain.Entities.Driver, DriverDto>(It.IsAny<Domain.Entities.Driver>()))
                .Returns(updatedDto);

            // Act
            var result = await service.UpdateAsync(updateDriverDto);

            // Assert
            Assert.Equal(updatedDto.Id, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Domain.Entities.Driver>>();
            var mockMapper = new Mock<IMapper>();
            var service = new DriverService(mockRepository.Object, mockMapper.Object, _randomDriverServiceMock.Object);
            
            var entityToDelete =CreateDriver();

            mockRepository.Setup(repo => repo.GetAsync(entityToDelete.Id)).ReturnsAsync(entityToDelete);
            mockRepository.Setup(repo => repo.DeleteAsync(entityToDelete)).ReturnsAsync(true);

            // Act
            var result = await service.DeleteAsync(entityToDelete.Id);

            // Assert
            Assert.True(result);
        }


        private AddDriverDto CreateAddDriverDto()
        {
            var driver = new AddDriverDto()
            {
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }


        private UpdateDriverDto CreateUpdateDriverDto(Guid id)
        {
            var driver = new UpdateDriverDto()
            {
                Id = id,
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }

        private DriverDto CreateDriverDto(Guid id)
        {
            var driver = new DriverDto()
            {
                Id = id,
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }


        private Domain.Entities.Driver CreateDriver()
        {
            var driver = new Domain.Entities.Driver()
            {
                Id = Guid.NewGuid(),
                Email = "mahmoud@gmail.com",
                FirstName = "Mahmoud",
                LastName = "Ragab",
                PhoneNumber = "+9999999"
            };

            return driver;
        }
    }
}
