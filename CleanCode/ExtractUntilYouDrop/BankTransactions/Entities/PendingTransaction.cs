
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class PendingTransaction : Transaction
    {
        public override bool IsCompleted => false;

        // Construct NEW pending transaction
        public PendingTransaction(string accountNumber,
            Date transactionDate,
            decimal amount,
            string description,
            TransactionType transactionType)
            : this(Guid.NewGuid(),
                accountNumber,
                transactionDate,
                amount,
                description,
                transactionType,
                amount > 0)
        { }

        // Construct EXISTING pending transaction
        public PendingTransaction(Guid id,
            string accountNumber,
            Date transactionDate,
            decimal amount,
            string description,
            TransactionType transactionType,
            bool isExcluded)
            : base(id,
                accountNumber,
                transactionDate,
                amount,
                description,
                transactionType,
                isExcluded)
        { }

        public PendingTransaction(Guid id,
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
