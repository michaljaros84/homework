using Company.Schedule.Domain.Entities;
using Company.Schedule.Domain.Interfaces;
using Company.Schedule.Services.Domain.DTO;
using Company.Schedule.Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Schedule.Services
{
    public class CompanyScheduleService : ICompanyScheduleService
    {
        private readonly IRepository _repository;
        private readonly IDateCalculator _calculator;

        public CompanyScheduleService(IRepository repository, IDateCalculator calculator)
        {
            _repository = repository;
            _calculator = calculator;
        }

        public async Task<ScheduleResultDto> GetScheduleAsync(Guid companyId)
        {
            var company = await _repository.GetCompanyAsync(companyId).ConfigureAwait(false);

            if (company != null)
            {
                return new ScheduleResultDto
                {
                    CompanyId = company.Id,
                    Schedule = company.Schedule.Select(s => s.Date.ToString("dd/MM/yyyy"))
                };
            }

            return null;
        }

        public async Task<IEnumerable<ScheduleResultDto>> GetAllSchedulesAsync()
        {
            var companies = await _repository.GetAllSchedules().ConfigureAwait(false);

            return companies.Select(s => new ScheduleResultDto { 
                CompanyId = s.Id,
                Schedule = s.Schedule.Select(s => s.Date.ToString("dd/MM/yyyy"))
            });
        }

        public async Task<ScheduleResultDto> InsertCompanyAsync(CompanyDto company)
        {
            CompanyEntity companyEntity = await _repository.GetCompanyAsync(company.Id).ConfigureAwait(false);
            Market market = await GetMarketAsync(company, companyEntity).ConfigureAwait(false);
            ScheduleResultDto res = null;

            if (CorrectCompanyTypeForMarket(company, market))
            {
                List<ScheduledDate> newSchedule = CalculateScheduleEntities(company, market);

                companyEntity = await HandleScheduleInsertAsync(companyEntity, company, market, newSchedule).ConfigureAwait(false);

                res = new ScheduleResultDto
                {
                    CompanyId = companyEntity.Id,
                    Schedule = companyEntity.Schedule.Select(s => s.Date.ToString("dd/MM/yyyy"))
                };
            }

            return res;
        }

        private async Task<Market> GetMarketAsync(CompanyDto company, CompanyEntity companyEntity)
        {
            Market market = null;

            if (companyEntity != null && companyEntity.Market.Name == company.MarketName)
            {
                market = companyEntity.Market;
            }
            else
            {
                market = await _repository.GetMarketAsync(company.MarketName).ConfigureAwait(false);
            }

            return market;
        }

        private List<ScheduledDate> CalculateScheduleEntities(CompanyDto company, Market market)
        {
            if (company.StartDate.HasValue)
            {
                var scheduledDates = _calculator.GetScheduledDates(company.StartDate.Value, market.MarketDays.Select(c => c.DaysOffset));
                return scheduledDates.Select(d => new ScheduledDate { Date = d }).ToList();
            }

            return new List<ScheduledDate>();                
        }

        private async Task<CompanyEntity> HandleScheduleInsertAsync(CompanyEntity companyEntity, CompanyDto company, Market market, List<ScheduledDate> newSchedule)
        {
            if (companyEntity == null)
            {
                companyEntity = new CompanyEntity
                {
                    Id = company.Id,
                    CompanyNumber = company.CompanyNumber,
                    CompanyType = (int)company.CompanyType,
                    Market = market,
                    Name = company.Name
                };

                await _repository.InsertCompanyAsync(companyEntity).ConfigureAwait(false);
            }
            else
            {
                companyEntity.CompanyNumber = company.CompanyNumber;
                companyEntity.CompanyType = (int)company.CompanyType;
                companyEntity.Name = company.Name;
                companyEntity.Market = market;
            }
           
            await _repository.UpdateScheduleAsync(companyEntity, newSchedule).ConfigureAwait(false);

            return companyEntity;
        }

        private bool CorrectCompanyTypeForMarket(CompanyDto comapny, Market market)
        {
            return market.Small && (comapny.CompanyType.ToString() == nameof(market.Small))
                || market.Medium && (comapny.CompanyType.ToString() == nameof(market.Medium))
                || market.Large && (comapny.CompanyType.ToString() == nameof(market.Large));
        }
    }
}
