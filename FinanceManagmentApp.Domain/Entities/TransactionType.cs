namespace FinanceManagmentApp.Domain.Entities
{
    public sealed class TransactionType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsExpense { get; set; }
        public IEnumerable<FinancialOperation> FinancialOperations { get; set; } = new List<FinancialOperation>();
    }
}
