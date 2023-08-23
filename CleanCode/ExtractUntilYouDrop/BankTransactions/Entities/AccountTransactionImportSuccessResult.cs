
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class AccountTransactionImportSuccessResult : AccountTransactionImportResult
    {
        public int CompletedCount => CompletedTransactions.Count;
        public int PendingCount => PendingTransactions.Count;
        public CompletedAccountTxList CompletedTransactions { get; }
        public PendingAccountTxColl PendingTransactions { get; }


        public AccountTransactionImportSuccessResult(Account account)
            : base(account)
        {
            CompletedTransactions = new CompletedAccountTxList(account);
            PendingTransactions = new PendingAccountTxColl(account);
        }

        public AccountTransactionImportSuccessResult(Account account,
            CompletedAccountTxList completedTx)
            : this(account)
        {
            CompletedTransactions = completedTx;
        }

        public AccountTransactionImportSuccessResult(Account account,
            CompletedAccountTxList completedTx,
            PendingAccountTxColl pendingTx)
            : this(account, completedTx)
        {
            PendingTransactions = pendingTx;
        }

        public AccountTransactionImportSuccessResult(OrderedAccountTransactions transactions)
            : this(transactions.Account)
        {
            CompletedTransactions = transactions.CompletedTransactions;
            PendingTransactions = transactions.PendingTransactions;
        }
    }
}
