using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet("daySummary")]
        public async Task<ActionResult<SummaryDTO>> GetDaySummary([FromBody] DateOnly date)
        {
            var summary = await _summaryService.GetDaySummaryAsync(User, date);
            return Ok(summary);
        }

        [HttpGet("dateRangeSummary")]
        public async Task<ActionResult<SummaryDTO>> GetDateRangeSummary([FromBody] DateOnly from, DateOnly to)
        {
            var summary = await _summaryService.GetDateRangeSummaryAsync(User, from, to);
            return Ok(summary);
        }
    }
}
