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

        [HttpPost("daySummary")]
        public async Task<ActionResult<SummaryDTO>> GetDaySummary([FromBody] DateOnly date)
        {
            var summary = await _summaryService.GetDaySummaryAsync(User, date);
            return Ok(summary);
        }

        [HttpPost("dateRangeSummary")]
        public async Task<ActionResult<SummaryDTO>> GetDateRangeSummary([FromBody] DateRangeDTO dateRange)
        {
            var summary = await _summaryService.GetDateRangeSummaryAsync(User, dateRange);
            return Ok(summary);
        }
    }
}
