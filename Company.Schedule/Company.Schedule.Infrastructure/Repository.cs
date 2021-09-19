
using Company.Schedule.Domain.Entities;
using Company.Schedule.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Schedule.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly CompanyScheduleContext _context;

        public Repository(CompanyScheduleContext context)
        {
            _context = context;
        }

        public async Task InsertCompanyAsync(CompanyEntity company)
        {
            var exists = _context.Companies
                .Any(c => c.CompanyNumber == company.CompanyNumber);

            if (!exists)
            {
                await _context.Companies.AddAsync(company).ConfigureAwait(false);   
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<CompanyEntity> GetCompanyAsync(Guid companyId)
        {
            var res = await _context.Companies
                .Include(c => c.Market)
                .ThenInclude(m => m.MarketDays)
                .Include(c => c.Schedule)
                .FirstOrDefaultAsync(c => c.Id == companyId)
                .ConfigureAwait(false);

            return res;
        }

        public async Task<IEnumerable<CompanyEntity>> GetAllSchedules()
        {
            var res = await _context.Companies
               .Include(c => c.Market)
               .ThenInclude(m => m.MarketDays)
               .Include(c => c.Schedule)
               .ToListAsync()
               .ConfigureAwait(false);

            return res;
        }

        public async Task<Market> GetMarketAsync(string marketName)
        {
            var res = await _context.Markets
                .Include(c => c.MarketDays)
                .FirstOrDefaultAsync(c => c.Name == marketName)
                .ConfigureAwait(false);

            return res;
        }

        public async Task UpdateScheduleAsync(CompanyEntity company, ICollection<ScheduledDate> newSchedule)
        {
            company.Schedule = newSchedule;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
