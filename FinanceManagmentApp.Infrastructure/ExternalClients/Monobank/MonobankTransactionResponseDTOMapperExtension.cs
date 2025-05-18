using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.Infrastructure.ExternalClients.Monobank
{
    internal static class MonobankTransactionResponseDTOMapperExtension
    {
        public static FinancialOperation ToFinancialOperation(this MonobankTransactionResponseDTO monobankTransaction, Dictionary<int, Guid> mccToTransactionType, Guid userId)
        {
            return new FinancialOperation()
            {
                Id = Guid.NewGuid(),
                Amount = monobankTransaction.Amount,
                UserComment = MergeDescription(monobankTransaction),
                TransactionTypeId = ConvertMccToTransactionTypeId(monobankTransaction.Mcc, mccToTransactionType),
                UserId = userId
            };
        }

        private static Guid ConvertMccToTransactionTypeId(int mcc, Dictionary<int, Guid> mccToTransactionType)
        {
            try
            {
                return mccToTransactionType[mcc];
            }
            catch (KeyNotFoundException)
            {
                return mccToTransactionType[-1];
            }
        }

        private static string MergeDescription(MonobankTransactionResponseDTO monobankTransaction)
        {
            return $"{monobankTransaction.Comment} - {monobankTransaction.Description} - {monobankTransaction.CounterName}";
        }
    }
}
