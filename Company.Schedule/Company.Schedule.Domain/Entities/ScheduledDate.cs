using System;
using System.ComponentModel.DataAnnotations;

namespace Company.Schedule.Domain.Entities
{
    public class ScheduledDate
    {
        [Key]
        public long Id { get; set; }

        public Guid CompanyEntityId {  get; set; }

        public DateTime Date { get; set; }
    }
}
