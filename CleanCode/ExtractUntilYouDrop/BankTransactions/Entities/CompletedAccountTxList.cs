using CleanCode.ExtractUntilYouDrop.BankTransactions.Exceptions;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class CompletedAccountTxList : AccountTransactionList<CompletedTransaction>
    {
        public decimal OpeningBalance => CalcOpeningBalance();
        public decimal ClosingBalance => CalcClosingBalance();


        public CompletedAccountTxList(Account account)
            : base(account)
        { }

        public CompletedAccountTxList(Account account,
            IList<CompletedTransaction> transactions)
            : this(account)
        {
            foreach (var transation in transactions)
                Add(transation);
        }

        public CompletedAccountTxList(Account account,
            params CompletedTransaction[] transactions)
            : this(account, transactions.ToList())
        { }


        public override void Add(CompletedTransaction newTx)
        {
            var lastTx = Transactions.LastOrDefault();
            if (lastTx != null)
                if (lastTx.Balance + newTx.Amount != newTx.Balance)
                    throw new InvalidTransactionOrder();
            base.Add(newTx);
        }

        public CompletedAccountTxList Add(CompletedAccountTxList newTx)
        {
            // Note: This does not add the transactions to the current instance
            // but returns a new instance containing the added values.
            CheckAdd(newTx);
            var combinedTx = new CompletedAccountTxList(Account, Transactions);
            foreach (var tx in newTx.Transactions)
                combinedTx.Add(tx);
            return combinedTx;
        }

        public CompletedAccountTxList GetTransactionsAfter(CompletedAccountTxList olderTx)
        {
            // TODO: Check on the last 2 transactions to make sure that we really
            // do capture only new transactions here.
            const int TxCompareCount = 2;
            if (!olderTx.Any())
                return this;
            var newestOldTx = CalcNewestOldTransactions(olderTx, TxCompareCount);
            var newTx = new CompletedAccountTxList(Account);

            var isNewTx = false;
            var compareCount = 0;
            for (var i = 0; i < Transactions.Count; i++)
            {
                if (compareCount == newestOldTx.Count)
                    isNewTx = true;
                if (isNewTx)
                {
                    newTx.Add(Transactions[i]);
                    continue;
                }
                if (compareCount > 0)
                    if (!newestOldTx[compareCount].Equals(Transactions[i]))
                        compareCount = 0;
                if (newestOldTx[compareCount].Equals(Transactions[i]))
                    compareCount++;
            }
            return newTx;
        }

        private IList<CompletedTransaction> CalcNewestOldTransactions(CompletedAccountTxList olderTx,
            int txCount)
        {
            if (olderTx.Count <= txCount)
                return olderTx.Transactions;
            else
                return olderTx.Transactions.Skip(olderTx.Count - txCount).Take(txCount).ToList();
        }

        private decimal CalcOpeningBalance()
        {
            if (!Transactions.Any())
                return 0;
            var firstTx = Transactions.First();
            return firstTx.Balance - firstTx.Amount;
        }

        private decimal CalcClosingBalance()
        {
            if (!Transactions.Any())
                return 0;
            return Transactions.Last().Balance;
        }

        // private void CheckTransactionOrder()
        // {
        //     decimal prevTxBalance = 0;
        //     for (int i = 0; i < Transactions.Count; i++)
        //     {
        //         if (i == 0)
        //             prevTxBalance = OpeningBalance;
        //         var currentTx = Transactions[i];
        //         if (prevTxBalance + currentTx.Amount != currentTx.Balance)
        //             throw new InvalidTransactionOrder();
        //         prevTxBalance = currentTx.Balance;
        //     }
        // }
    }
}
