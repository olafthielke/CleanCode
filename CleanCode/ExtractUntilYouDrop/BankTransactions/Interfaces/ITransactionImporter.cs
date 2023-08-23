using CleanCode.ExtractUntilYouDrop.BankTransactions.Entities;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Interfaces
{
    public interface ITransactionImporter
    {
        Task<AccountTransactionImportResult> ImportTransactions(ImportAccount account);
    }
}
