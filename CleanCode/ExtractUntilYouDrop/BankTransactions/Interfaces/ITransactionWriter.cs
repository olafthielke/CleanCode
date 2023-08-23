
using CleanCode.ExtractUntilYouDrop.BankTransactions.Entities;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Interfaces
{
    public interface ITransactionWriter
    {
        Task AddCompletedTransaction(CompletedTransaction transaction);
        Task AddPendingTransaction(PendingTransaction transaction);
        Task DeletePendingTransactions(Account account);
    }
}
