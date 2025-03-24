﻿using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services.Abstractions
{
    public interface ISummaryService
    {
        Task<SummaryDTO> GetDaySummary(DateOnly dateOnly);
        Task<SummaryDTO> GetDateRangeSummary(DateRangeDTO dateRange);
    }
}
