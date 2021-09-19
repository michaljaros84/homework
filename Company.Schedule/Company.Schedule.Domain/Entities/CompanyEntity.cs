using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Schedule.Domain.Entities
{
    public class CompanyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string CompanyNumber { get; set; }

        [Required]
        public int CompanyType { get; set; }

        public Market Market { get; set; }

        public ICollection<ScheduledDate> Schedule { get; set; } = new List<ScheduledDate>();
    }
}
