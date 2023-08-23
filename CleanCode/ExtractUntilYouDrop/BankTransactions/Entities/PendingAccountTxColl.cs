
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class PendingAccountTxColl
    {
        private List<PendingTransaction> _transactions;

        public IEnumerable<PendingTransaction> Transactions => _transactions;
        public Account Account { get; private set; }
        public string BankName => Account.BankName;
        public string AccountNumber => Account.AccountNumber;
        public int Count => _transactions.Count;
        public decimal Total => _transactions.Sum(x => x.Amount);
        public bool Any() => _transactions.Any();
        public void Reverse() => _transactions.Reverse();


        public PendingAccountTxColl(Account account)
        {
            Account = account;
            _transactions = new List<PendingTransaction>();
        }

        public PendingAccountTxColl(Account account, IEnumerable<PendingTransaction> transactions)
        {
            Account = account;
            _transactions = new List<PendingTransaction>(transactions);
        }

        public void Add(PendingTransaction pTx)
        {
            _transactions.Add(pTx);
        }


        public PendingAccountTxColl EnsureTransactionsInDateRange(DateRangeQuery dateRange)
        {
            var dateRangeTx = _transactions.Where(x => x.TransactionDate >= dateRange.StartDate
                                                       && x.TransactionDate <= dateRange.EndDate);
            return new PendingAccountTxColl(Account, dateRangeTx);
        }
    }
}
