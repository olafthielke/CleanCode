using CleanCode.ExtractUntilYouDrop.BankTransactions.Exceptions;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class OrderedAccountTransactions
    {
        public Account Account { get; }
        public DateRangeQuery DateRange { get; }
        public string BankName => Account.BankName;
        public string AccountNumber => Account.AccountNumber;
        public Date StartDate => DateRange.StartDate;
        public Date EndDate => DateRange.EndDate;
        public int Count => CompletedTransactions.Count + PendingTransactions.Count;
        public decimal Total => CompletedTransactions.Total + PendingTransactions.Total;

        public CompletedAccountTxList CompletedTransactions { get; }
        public PendingAccountTxColl PendingTransactions { get; }

        public OrderedAccountTransactions(Account account,
            DateRangeQuery dateRange)
        {
            Account = account;
            DateRange = dateRange;
            CompletedTransactions = new CompletedAccountTxList(Account);
            PendingTransactions = new PendingAccountTxColl(Account);
        }

        public OrderedAccountTransactions(Account account,
            DateRangeQuery dateRange,
            CompletedAccountTxList completedTransactions)
            : this(account, dateRange)
        {
            CompletedTransactions = completedTransactions;
            ValidateAccountForCompletedTransactions();
        }

        public OrderedAccountTransactions(Account account,
            DateRangeQuery dateRange,
            CompletedAccountTxList completedTransactions,
            PendingAccountTxColl pendingTransactions)
            : this(account, dateRange)
        {
            CompletedTransactions = completedTransactions;
            ValidateAccountForCompletedTransactions();
            PendingTransactions = pendingTransactions;
            ValidateAccountForPendingTransactions();
        }


        public void AddCompletedTransaction(CompletedTransaction tx)
        {
            CompletedTransactions.Add(tx);
        }

        public void AddPendingTransaction(PendingTransaction pTx)
        {
            PendingTransactions.Add(pTx);
        }


        private void ValidateAccountForCompletedTransactions()
        {
            if (CompletedTransactions.BankName != Account.BankName)
                throw new MismatchedBankName(Account.BankName, CompletedTransactions.BankName);
            if (CompletedTransactions.AccountNumber != Account.AccountNumber)
                throw new MismatchedAccountNumber(Account.AccountNumber, CompletedTransactions.AccountNumber);
        }

        private void ValidateAccountForPendingTransactions()
        {
            if (PendingTransactions.BankName != Account.BankName)
                throw new MismatchedBankName(Account.BankName, PendingTransactions.BankName);
            if (PendingTransactions.AccountNumber != Account.AccountNumber)
                throw new MismatchedAccountNumber(Account.AccountNumber, PendingTransactions.AccountNumber);
        }
    }
}
