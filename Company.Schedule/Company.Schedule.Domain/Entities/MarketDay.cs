using System.ComponentModel.DataAnnotations;

namespace Company.Schedule.Domain.Entities
{
    public class MarketDay
    {
        [Key]
        public long Id { get; set; }

        public long MarketId { get; set; }

        public int DaysOffset { get; set; }
    }
}
