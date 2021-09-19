using Company.Schedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.Schedule.Infrastructure
{
    public static class SeedExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Market>().HasData(
                new Market { Id = 1, Name = "Denmark", Small = true, Medium = true, Large = true },
                new Market { Id = 2, Name = "Norway", Small = true, Medium = true, Large = true },
                new Market { Id = 3, Name = "Sweden", Small = true, Medium = true, Large = false },
                new Market { Id = 4, Name = "Finland", Small = false, Medium = false, Large = true }
                );

            modelBuilder.Entity<MarketDay>().HasData(
                new MarketDay { Id = 1, DaysOffset = 1, MarketId = 1 },
                new MarketDay { Id = 2, DaysOffset = 5, MarketId = 1 },
                new MarketDay { Id = 3, DaysOffset = 10, MarketId = 1 },
                new MarketDay { Id = 4, DaysOffset = 15, MarketId = 1 },
                new MarketDay { Id = 5, DaysOffset = 20, MarketId = 1 },

                new MarketDay { Id = 6, DaysOffset = 1, MarketId = 2 },
                new MarketDay { Id = 7, DaysOffset = 5, MarketId = 2 },
                new MarketDay { Id = 8, DaysOffset = 10, MarketId = 2 },
                new MarketDay { Id = 9, DaysOffset = 20, MarketId = 2 },

                new MarketDay { Id = 10, DaysOffset = 1, MarketId = 3 },
                new MarketDay { Id = 11, DaysOffset = 7, MarketId = 3 },
                new MarketDay { Id = 12, DaysOffset = 14, MarketId = 3 },
                new MarketDay { Id = 13, DaysOffset = 28, MarketId = 3 },

                new MarketDay { Id = 14, DaysOffset = 1, MarketId = 4 },
                new MarketDay { Id = 15, DaysOffset = 5, MarketId = 4 },
                new MarketDay { Id = 16, DaysOffset = 10, MarketId = 4 },
                new MarketDay { Id = 17, DaysOffset = 15, MarketId = 4 },
                new MarketDay { Id = 18, DaysOffset = 20, MarketId = 4 }
                );
        }
    }
}
