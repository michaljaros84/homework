using Company.Schedule.Domain.Entities;
using Company.Schedule.Domain.Interfaces;
using Company.Schedule.Services;
using Company.Schedule.Services.Domain;
using Company.Schedule.Services.Domain.DTO;
using Company.Schedule.Services.Domain.Enums;
using Company.Schedule.Services.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Schedule.Tests
{
    [TestClass]
    // This is here just to show how I usually setup tests. Obviously there would be more paths tested. no time really
    public class CompanyScheduleServiceTests
    {
        private Mock<IRepository> _repoMock;
        private IDateCalculator _dateCalculator;
        private ICompanyScheduleService _service;

        [TestInitialize]
        public void Setup()
        {
            _dateCalculator = new DateCalculator();
            _repoMock = new Mock<IRepository>();
            _service = new CompanyScheduleService(
                _repoMock.Object,
                _dateCalculator);
        }

        [TestMethod]
        public async Task InsertCompanyHappyPathTest()
        {
            var dto = new CompanyDto
            {
                CompanyNumber = "0123456789",
                Id = new Guid("aad7a630-af1c-4952-9cb4-44b8b847853b"),
                CompanyType = CompanyTypeEnum.Small,
                MarketName = "Denmark",
                StartDate = new DateTime(2021,6,1)
            };

            _repoMock.Setup(c => c.GetCompanyAsync(It.IsAny<Guid>())).Returns(Task.FromResult<CompanyEntity>(null));
            _repoMock.Setup(c => c.GetMarketAsync(It.IsAny<string>())).Returns(Task.FromResult(new Market
            {
                Small = true,
                MarketDays = new List<MarketDay> { new MarketDay { DaysOffset = 2 }, new MarketDay { DaysOffset = 7 } }
            }));
            var res = await _service.InsertCompanyAsync(dto);

            _repoMock.Verify(c => c.InsertCompanyAsync(It.IsAny<CompanyEntity>()), Times.Once);
            _repoMock.Verify(c => c.UpdateScheduleAsync(It.IsAny<CompanyEntity>(), It.IsAny<ICollection<ScheduledDate>>()), Times.Once);
        }
    }
}
