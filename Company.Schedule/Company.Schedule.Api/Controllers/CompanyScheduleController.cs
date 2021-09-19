using Company.Schedule.Services.Domain.DTO;
using Company.Schedule.Services.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Company.Schedule.Api.Controllers
{
    //no authorization was mentioned but I guess some flavour would be implemented and distincted by read/write access
    //[Authorization(READONLY)]
    public class CompanyScheduleController : Controller
    {
        private readonly ICompanyScheduleService _schedulerService;

        public CompanyScheduleController(ICompanyScheduleService schedulerService)
        {
            _schedulerService = schedulerService;
        }

        [HttpPost("insert")]
        //[Authorization(WRITER)]
        public async Task<IActionResult> InsertCountrySchedule(CompanyDto company)
        {
            var res = await _schedulerService.InsertCompanyAsync(company);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountrySchedule(Guid id)
        {
            var res = await _schedulerService.GetScheduleAsync(id);

            if (res == null)
            {
                return NotFound($"No schedule for id {id}");
            }

            return Ok(res);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllSchedules()
        {
            var res = await _schedulerService.GetAllSchedulesAsync();  

            return Ok(res);
        }
    }
}
