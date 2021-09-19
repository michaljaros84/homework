using System;
using System.Collections.Generic;

namespace Company.Schedule.Services.Domain.DTO
{
    public class ScheduleResultDto
    {
        public Guid CompanyId { get; set; }

        public IEnumerable<string> Schedule { get; set; }
    }
}
