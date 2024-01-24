using Application.Services;
using AutoFixture.AutoMoq;
using AutoFixture;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using FluentAssertions;
using Domain.Exceptions;

namespace Exchange.UnitTests.Services
{
    public class CurrencyRateServiceTests
    {
        private readonly Mock<ICurrencyRateRepository> _repositoryMock;
        private readonly CurrencyRateService _service;
        private readonly Fixture _fixture;

        public CurrencyRateServiceTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<ICurrencyRateRepository>();
            _service = new CurrencyRateService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetCurrencyRatesWithChangesAsync_GivenFirstDayOfYear_ReturnsNoPreviousDayDataException()
        {
            // Arrange
            var firstDayOfYear = new DateTime(2010, 01, 01);
            var mockedDataToday = "<xml>mocked data for today</xml>";
            var mockedDataYesterday = "<xml>mocked data for yesterday</xml>";

            _repositoryMock.Setup(repo => repo.GetCurrencyDataByDateAsync(firstDayOfYear))
                           .ReturnsAsync(mockedDataToday);
            _repositoryMock.Setup(repo => repo.GetCurrencyDataByDateAsync(firstDayOfYear.AddDays(-1)))
                           .ReturnsAsync(mockedDataYesterday);

            // Act
            Func<Task> act = async () => await _service.GetCurrencyRatesWithChangesAsync(firstDayOfYear);

            // Assert
            await act.Should().ThrowAsync<NoPreviousDayDataException>()
                      .WithMessage("No previous day data available for the first day of the year.");
        }

        [Fact]
        public async Task GetCurrencyRatesWithChangesAsync_GivenFutureDate_ThrowsInvalidRequestException()
        {
            // Arrange
            var futureDate = DateTime.Now.AddDays(1);

            // Act
            Func<Task> act = async () => await _service.GetCurrencyRatesWithChangesAsync(futureDate);

            // Assert
            await act.Should().ThrowAsync<InvalidRequestException>();
        }

        [Fact]
        public async Task GetCurrencyRatesWithChangesAsync_GivenDateWithNoData_ThrowsNoDataAvailableException()
        {
            // Arrange
            var validDate = new DateTime(1014, 2, 1);
            _repositoryMock.Setup(repo => repo.GetCurrencyDataByDateAsync(It.IsAny<DateTime>()))
                           .ReturnsAsync("<xml>No Data</xml>");

            // Act
            Func<Task> act = async () => await _service.GetCurrencyRatesWithChangesAsync(validDate);

            // Assert
            await act.Should().ThrowAsync<NoDataAvailableException>();
        }
    }
}
