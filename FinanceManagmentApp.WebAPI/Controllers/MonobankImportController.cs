using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using FinanceManagmentApp.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MonobankImportController : ControllerBase
{
    private readonly IMonobankImportService _monobankImportService;

    public MonobankImportController(IMonobankImportService monobankImportService)
    {
        _monobankImportService = monobankImportService ?? throw new ArgumentNullException(nameof(monobankImportService));
    }

    [HttpPost]
    public async Task<ActionResult<SummaryDTO>> ImportFinancialOperations([FromBody] DateRangeDTO dateRange)
    {
        var userId = User.GetUserIdFromJwt();

        var summaryForImportedFinOps = await _monobankImportService.ImportFinancialOperations(userId
            , dateRange.StartDate.ToDateTime(TimeOnly.MinValue)
            , dateRange.EndDate.ToDateTime(TimeOnly.MinValue));

        return Ok(summaryForImportedFinOps);
    }
}
