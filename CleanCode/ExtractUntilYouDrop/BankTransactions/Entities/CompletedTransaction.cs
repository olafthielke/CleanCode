using System.Text;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class CompletedTransaction : Transaction
    {
        public Date ProcessedDate { get; }
        public string TransactionOrder { get; set; }
        public decimal Balance { get; protected set; }

        public override bool IsCompleted => true;

        // Construct NEW completed transaction
        public CompletedTransaction(string accountNumber,
            Date transactionDate,
            decimal amount,
            string description,
            TransactionType transactionType,
            Date processedDate,
            decimal balance)
            : this(Guid.NewGuid(),
                    accountNumber,
                    transactionDate,
                    amount,
                    description,
                    transactionType,
                    processedDate,
                    null,
                    balance,
                    amount > 0)
        { }

        // Construct EXISTING completed transaction
        public CompletedTransaction(Guid id,
            string accountNumber,
            DateTime transactionDate,
            decimal amount,
            string description,
            int transactionType,
            DateTime processedDate,
            string transactionOrder,
            decimal balance,
            bool isExcluded)
            : this(id,
                accountNumber,
                new Date(transactionDate),
                amount,
                description,
                (TransactionType)transactionType,
                new Date(processedDate),
                transactionOrder,
                balance,
                isExcluded)
        { }

        public CompletedTransaction(Guid id,
            string accountNumber,
            Date transactionDate,
            decimal amount,
            string description,
            TransactionType transactionType,
            Date processedDate,
            string transactionOrder,
            decimal balance,
            bool isExcluded)
            : base(id,
                accountNumber,
                transactionDate,
                amount,
                description,
                transactionType,
                isExcluded)
        {
            ProcessedDate = processedDate;
            TransactionOrder = transactionOrder;
            Balance = balance;
        }


        public override bool Equals(Object obj)
        {
            CompletedTransaction tx = obj as CompletedTransaction;
            if (tx == null)
                return false;
            else
                return AccountNumber.Trim() == tx.AccountNumber.Trim() &&
                       TransactionDate.ToString() == tx.TransactionDate.ToString() &&
                       Amount.ToString("{0:c2}") == tx.Amount.ToString("{0:c2}") &&
                       ProcessedDate.ToString() == tx.ProcessedDate.ToString() &&
                       Balance.ToString("{0:c2}") == tx.Balance.ToString("{0:c2}");
        }

        public override int GetHashCode()
        {
            var builder = new StringBuilder();
            builder.AppendFormat($"{AccountNumber}|");
            builder.AppendFormat($"{TransactionDate.ToString()}|");
            builder.AppendFormat($"{Amount:c2}|");
            builder.AppendFormat($"{ProcessedDate.ToString()}|");
            builder.AppendFormat($"{Balance:c2}");

            return builder.ToString().GetHashCode();
        }
    }
}
