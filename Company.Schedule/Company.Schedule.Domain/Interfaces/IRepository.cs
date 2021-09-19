using Company.Schedule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Schedule.Domain.Interfaces
{
    public interface IRepository
    {
        Task InsertCompanyAsync(CompanyEntity company);

        Task<CompanyEntity> GetCompanyAsync(Guid companyId);

        Task<Market> GetMarketAsync(string marketName);

        Task UpdateScheduleAsync(CompanyEntity company, ICollection<ScheduledDate> newSchedule);

        Task<IEnumerable<CompanyEntity>> GetAllSchedules();
    }
}
