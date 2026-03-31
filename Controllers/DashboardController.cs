using DataEntrySystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public DashboardController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet("reports")]
        public async Task<IActionResult> GetReports([FromQuery] string? search, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _businessService.GetMonthlyReportsAsync(search, from, to);
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _businessService.GetSummaryAsync(from, to);
            return Ok(result);
        }

        [HttpGet("expenses")]
        public async Task<IActionResult> GetExpenses([FromQuery] string? search, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _businessService.GetMonthlyReportsAsync(search, from, to);
            return Ok(result);
        }
    }
}
