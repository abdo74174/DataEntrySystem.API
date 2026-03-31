using DataEntrySystem.API.Models.DTOs;
using DataEntrySystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RevenuesController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public RevenuesController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _businessService.GetAllRevenuesAsync(search, from, to);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RevenueCreateDto revenueDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            await _businessService.CreateRevenueAsync(revenueDto, userId);
            return Ok(new { message = "Added Successfully" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] RevenueCreateDto revenueDto)
        {
            await _businessService.UpdateRevenueAsync(id, revenueDto);
            return Ok(new { message = "Updated Successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _businessService.DeleteRevenueAsync(id);
            return Ok(new { message = "Deleted Successfully" });
        }
    }
}
