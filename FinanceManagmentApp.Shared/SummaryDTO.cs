namespace FinanceManagmentApp.Shared
{
    public sealed class SummaryDTO
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public IEnumerable<FinancialOperationForUpdateAndSummaryDTO> Operations { get; set; } = new List<FinancialOperationForUpdateAndSummaryDTO>();
    }
}
