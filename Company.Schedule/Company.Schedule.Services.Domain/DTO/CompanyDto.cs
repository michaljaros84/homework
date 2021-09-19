using Company.Schedule.Services.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Company.Schedule.Services.Domain.DTO
{
    public class CompanyDto
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string CompanyNumber { get; set; }

        [Required]
        public CompanyTypeEnum CompanyType { get; set; }

        [Required]
        public string MarketName { get; set; }

        public DateTime? StartDate { get; set; }
    }
}
