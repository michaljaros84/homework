using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.Schedule.Domain.Entities
{
    public class Market
    {
        [Key]
        public long Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public bool Small { get; set; }

        public bool Medium { get; set; }

        public bool Large { get; set; }

        public ICollection<MarketDay> MarketDays { get; set; }
    }
}
