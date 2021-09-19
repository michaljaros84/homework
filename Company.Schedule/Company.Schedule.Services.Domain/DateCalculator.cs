using Company.Schedule.Services.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Company.Schedule.Services.Domain
{
    public class DateCalculator : IDateCalculator
    {
        public List<DateTime> GetScheduledDates(DateTime start, IEnumerable<int> offsets)
        {
            var res = new List<DateTime>();

            foreach (var offset in offsets)
            {
                res.Add(start.Date.AddDays(offset));
            }

            return res;
        }
    }
}
