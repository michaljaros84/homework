using Microsoft.EntityFrameworkCore;
using Company.Schedule.Domain.Entities;

namespace Company.Schedule.Infrastructure
{
    public class CompanyScheduleContext : DbContext
    {
        public DbSet<CompanyEntity> Companies { get; set; }

        public DbSet<Market> Markets { get; set; }

        public DbSet<ScheduledDate> ScheduledDates { get; set; }

        public DbSet<MarketDay> MarketDays { get; set; }

        public CompanyScheduleContext(DbContextOptions<CompanyScheduleContext> contextOptions) 
            : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CompanyEntity>().HasMany(c => c.Schedule);
            modelBuilder.Entity<Market>().HasMany(c => c.MarketDays);
            modelBuilder.Seed();
        }
    }
}
