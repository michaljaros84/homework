using Company.Schedule.Services.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Company.Schedule.Services.Domain.Interfaces
{
    public interface ICompanyScheduleService
    {
        Task<ScheduleResultDto> GetScheduleAsync(Guid companyId);

        Task<ScheduleResultDto> InsertCompanyAsync(CompanyDto company);

        Task<IEnumerable<ScheduleResultDto>> GetAllSchedulesAsync();
    }
}
