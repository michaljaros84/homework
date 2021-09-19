using System;
using System.Collections.Generic;

namespace Company.Schedule.Services.Domain.Interfaces
{
    public interface IDateCalculator
    {
        List<DateTime> GetScheduledDates(DateTime start, IEnumerable<int> offsets);
    }
}
