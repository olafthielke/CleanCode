
using CleanCode.ExtractUntilYouDrop.BankTransactions.Entities;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Interfaces
{
    public interface ITransactionReader
    {
        Task<CompletedAccountTxList> GetCompletedTransactions(Account account, DateRangeQuery dateRange);
        Task<CompletedAccountTxList> GetCompletedTransactions(Account account, Date startDate);
        Task<PendingAccountTxColl> GetPendingTransactions(Account account);
    }
}
