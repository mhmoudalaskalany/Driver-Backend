using AutoFixture;
using AutoMapper;
using Candidate.Common.DTO.Candidate;
using Driver.Application.Services.Driver;
using Driver.Common.Abstraction.UnitOfWork;
using Driver.Common.DTO.Driver;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace Candidate.Application.Unit.Tests.Service
{
    public class CandidateServiceTests : AutoFixtureBase
    {
        private readonly Mock<IUnitOfWork<Driver.Domain.Entities.Driver>> _uowMock;
        private readonly DriverService _candidateServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        public CandidateServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork<Driver.Domain.Entities.Driver>>();
            Fixture.Register(() => _uowMock.Object);

            _mapperMock = new Mock<IMapper>();
            Fixture.Register(() => _mapperMock.Object);

            _candidateServiceMock = new DriverService(_uowMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList()
        {
            // arrange
            var entities = Fixture.Build<Driver.Domain.Entities.Driver>().CreateMany();
            var mapped = Fixture.Build<DriverDto>().CreateMany().ToList();

            _uowMock.Setup(x => x.Repository.GetAllAsync(It.IsAny<Func<IQueryable<Driver.Domain.Entities.Driver>, IIncludableQueryable<Driver.Domain.Entities.Driver, object>>>()
                , It.IsAny<bool>())).Returns(Task.FromResult(entities));

            _mapperMock.Setup(x => x.Map<IEnumerable<Driver.Domain.Entities.Driver>, List<DriverDto>>(It.IsAny<IEnumerable<Driver.Domain.Entities.Driver>>()))
                .Returns(mapped);

            // act
            var result = await _candidateServiceMock.GetAllAsync();

            // assert
            Assert.True(result.Any());
        }
    }
}
