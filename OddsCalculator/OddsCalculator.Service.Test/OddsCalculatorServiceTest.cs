using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using OddsCalculator.Entity;
using OddsCalculator.Service.Interfaces;

namespace OddsCalculator.Service.Test
{
    public class OddsCalculatorServiceTest
    {
        private static IOddsCalculatorService oddsCalculatorService;
        private static Mock<IOptions<InputCount>> maxCountConfigMock;

        [SetUp]
        public void Setup()
        {
            maxCountConfigMock = new Mock<IOptions<InputCount>>();
            oddsCalculatorService = new OddsCalculatorService(maxCountConfigMock.Object);
            maxCountConfigMock.Setup(x => x.Value).Returns(new InputCount { TotalCount = 100, DrawnCount = 10 });
        }

        [Test]
        public async Task GetOddsAsync_PassWithValidInput()
        {
            int totalCount = 48;
            int drawnCount = 6;
            var oddsList = new List<MatchedOdds>
            {
                new MatchedOdds{ MainMatch = 6, PerOddsCount = 12271512 }, new MatchedOdds{ MainMatch = 5, PerOddsCount = 48696 },
                new MatchedOdds{ MainMatch = 4, PerOddsCount = 950 }, new MatchedOdds{ MainMatch = 3, PerOddsCount = 53 },
                new MatchedOdds{ MainMatch = 2, PerOddsCount = 7 }, new MatchedOdds{ MainMatch = 1, PerOddsCount = 2 },
            };

            var result = await oddsCalculatorService.GetOddsAsync(totalCount, drawnCount);
            Assert.That(result.IsSuccess);
            oddsList.Should().BeEquivalentTo(result.Data);
        }

        [Test]
        public async Task GetOddsAsync_FailWithNegativeTotalCount()
        {
            int totalCount = -48;
            int drawnCount = 6;

            var result = await oddsCalculatorService.GetOddsAsync(totalCount, drawnCount);
            Assert.IsNull(result.Data);
            Assert.That(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetOddsAsync_FailWithNegativeDrawnCount()
        {
            int totalCount = 48;
            int drawnCount = -6;

            var result = await oddsCalculatorService.GetOddsAsync(totalCount, drawnCount);
            Assert.IsNull(result.Data);
            Assert.That(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetOddsAsync_FailWithHigerDrawnCount()
        {
            int totalCount = 48;
            int drawnCount = 49;

            var result = await oddsCalculatorService.GetOddsAsync(totalCount, drawnCount);
            Assert.IsNull(result.Data);
            Assert.That(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetOddsAsync_FailWithOutofboundCount()
        {
            int totalCount = 101;
            int drawnCount = 11;

            var result = await oddsCalculatorService.GetOddsAsync(totalCount, drawnCount);
            Assert.IsNull(result.Data);
            Assert.That(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetOddsAsync_FailWithDefaultCount()
        {
            var result = await oddsCalculatorService.GetOddsAsync(It.IsAny<int>(), It.IsAny<int>());

            Assert.IsNull(result.Data);
            Assert.That(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetOddsAsync_FailWithException()
        {
            int totalCount = int.MaxValue;
            int drawnCount = int.MaxValue - 1;

            maxCountConfigMock.Setup(x => x.Value).Returns(It.IsAny<InputCount>());

            var result = await oddsCalculatorService.GetOddsAsync(totalCount, drawnCount);
            Assert.IsNull(result.Data);
            Assert.That(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.InternalServerError);
        }
    }
}