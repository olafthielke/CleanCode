namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public abstract class Transaction
    {
        public Guid Id { get; }
        public string AccountNumber { get; }
        public Date TransactionDate { get; }
        public decimal Amount { get; protected set; }
        public string Description { get; }
        public TransactionType TransactionType { get; protected set; }
        public bool IsExcluded { get; }
        public abstract bool IsCompleted { get; }


        // Construct NEW transactions
        protected Transaction(Guid id,
            string accountNumber,
            Date transactionDate,
            decimal amount,
            string description,
            TransactionType transactionType,
            bool isExcluded)
        {
            Id = id;
            AccountNumber = accountNumber;
            TransactionDate = transactionDate;
            Amount = amount;
            Description = description;
            TransactionType = transactionType;
            IsExcluded = isExcluded;
        }

        public Transaction(Guid id,
            string accountNumber,
            DateTime transactionDate,
            decimal amount,
            string description,
            int transactionType,
            bool isExcluded)
            : this(id,
                    accountNumber,
                    new Date(transactionDate),
                    amount,
                    description,
                    (TransactionType)transactionType,
                    isExcluded)
        { }
    }
}
