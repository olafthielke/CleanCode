using CleanCode.ExtractUntilYouDrop.BankTransactions.Exceptions;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public abstract class AccountTransactionList<T> where T : Transaction
    {
        public IList<T> Transactions { get; }

        public Account Account { get; }
        public string BankName => Account.BankName;
        public string AccountNumber => Account.AccountNumber;
        public int Count => Transactions.Count;
        public decimal Total => Transactions.Sum(x => x.Amount);


        protected AccountTransactionList(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            Account = account;
            Transactions = new List<T>();
        }

        protected AccountTransactionList(Account account,
            IEnumerable<T> transactions)
            : this(account)
        {
            foreach (var transation in transactions)
                Add(transation);
        }

        protected AccountTransactionList(Account account,
            params T[] transactions)
            : this(account, transactions.ToList())
        { }


        public bool Any() => Transactions.Any();


        public virtual void Add(T newTx)
        {
            Transactions.Add(newTx);
        }

        public virtual void Add(IEnumerable<T> newTx)
        {
            foreach (var tx in newTx)
                Add(tx);
        }

        protected void CheckAdd(AccountTransactionList<T> newTx)
        {
            if (BankName != newTx.BankName)
                throw new MismatchedBankName(BankName, newTx.BankName);
            if (AccountNumber != newTx.AccountNumber)
                throw new MismatchedAccountNumber(AccountNumber, newTx.AccountNumber);
        }
    }
}
