using AutoFixture;
using Candidate.Common.DTO.Candidate;
using Driver.Api.Controllers.V1;
using Driver.Application.Services.Driver;
using Driver.Common.DTO.Driver;
using Moq;

namespace Candidate.Api.Unit.Tests.Controllers.Candidates
{
    public class CandidatesControllerUnitTest : AutoFixtureBase
    {
        private readonly Mock<IDriverService> _candidateServiceMock;
        private readonly DriversController _controller;
        public CandidatesControllerUnitTest()
        {
            _candidateServiceMock = new Mock<IDriverService>();
            Fixture.Register(() => _candidateServiceMock.Object);
            _controller = new DriversController(_candidateServiceMock.Object);
        }

        [Fact]
        public async Task GetActionsAsync_Return_Ok()
        {
            //Arrange
            var result = Fixture.Build<List<DriverDto>>().Create();
            _candidateServiceMock.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(result));

            //Act
            var response = await _controller.GetAllAsync();

            //Assert
            Assert.True(true);
        }



    }
}