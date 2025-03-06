using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using FinanceManagmentApp.WebAPI.Extensions;
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
            var userId = User.GetUserIdFromJwt();

            var summary = await _summaryService.GetDaySummaryAsync(userId, date);
            return Ok(summary);
        }

        [HttpPost("dateRangeSummary")]
        public async Task<ActionResult<SummaryDTO>> GetDateRangeSummary([FromBody] DateRangeDTO dateRange)
        {
            var userId = User.GetUserIdFromJwt();

            var summary = await _summaryService.GetDateRangeSummaryAsync(userId, dateRange);
            return Ok(summary);
        }
    }
}
