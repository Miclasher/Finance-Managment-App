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
                Amount = Math.Abs(monobankTransaction.Amount) / 100m,
                UserComment = MergeDescription(monobankTransaction),
                TransactionTypeId = ConvertMccToTransactionTypeId(monobankTransaction, mccToTransactionType),
                Date = DateTimeOffset.FromUnixTimeSeconds(monobankTransaction.Time).DateTime,
                UserId = userId
            };
        }

        private static Guid ConvertMccToTransactionTypeId(MonobankTransactionResponseDTO monobankTransaction, Dictionary<int, Guid> mccToTransactionType)
        {
            if (monobankTransaction.Amount > 0)
            {
                return mccToTransactionType[-1];
            }
            else
            {
                try
                {
                    return mccToTransactionType[monobankTransaction.Mcc];
                }
                catch (KeyNotFoundException)
                {
                    return mccToTransactionType[-2];
                }
            }
        }

        private static string MergeDescription(MonobankTransactionResponseDTO monobankTransaction)
        {
            return $"{monobankTransaction.Comment} - {monobankTransaction.Description} - {monobankTransaction.CounterName}";
        }
    }
}
