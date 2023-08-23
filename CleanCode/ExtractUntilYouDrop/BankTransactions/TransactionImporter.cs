using CleanCode.ExtractUntilYouDrop.BankTransactions.Entities;
using CleanCode.ExtractUntilYouDrop.BankTransactions.Exceptions;
using CleanCode.ExtractUntilYouDrop.BankTransactions.Interfaces;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions
{
    public class TransactionImporter : ITransactionImporter
    {
        public ITransactionReader TransactionReader { get; }
        public ITransactionWriter TransactionWriter { get; }
        private IBankTransactionClientFactory BankClientFactory { get; }
        public ILocalTimeProvider LocalTimeProvider { get; }

        private Date Today => new(LocalTimeProvider.Today);


        public TransactionImporter(ITransactionReader transactionReader,
            ITransactionWriter transactionWriter,
            IBankTransactionClientFactory bankClientFactory,
            ILocalTimeProvider localTimeProvider)
        {
            TransactionReader = transactionReader;
            TransactionWriter = transactionWriter;
            BankClientFactory = bankClientFactory;
            LocalTimeProvider = localTimeProvider;
        }


        public async Task<AccountTransactionImportResult> ImportTransactions(ImportAccount account)
        {
            try
            {
                // Calculate the latest transaction period
                var dateRange = new DateRangeCalculator(Today).LatestPeriod;

                // Get Saved Transactions
                var tx = await TransactionReader.GetCompletedTransactions(account, dateRange.StartDate);
                var pTx = await TransactionReader.GetPendingTransactions(account);
                var savedTx = new OrderedAccountTransactions(account, dateRange, tx, pTx);

                // Login to bank...
                var bankClient = await BankClientFactory.GetLoggedInClient(account.Credential);
                // ...and get the latest transactions.
                var bankTx = await bankClient.GetTransactions(account, dateRange);

                // Calculate the new bank transactions...
                var newTx = bankTx.CompletedTransactions.GetTransactionsAfter(savedTx.CompletedTransactions);
                SetTransactionOrder(newTx, savedTx.CompletedTransactions);
                // ...and then save them.
                if (newTx.Any())
                    foreach (var ctx in newTx.Transactions)
                        await TransactionWriter.AddCompletedTransaction(ctx);

                // Delete old pending transactions...
                await TransactionWriter.DeletePendingTransactions(bankTx.PendingTransactions.Account);
                // ...and then save the latest ones.
                if (bankTx.PendingTransactions.Any())
                    foreach (var ptx in bankTx.PendingTransactions.Transactions)
                        await TransactionWriter.AddPendingTransaction(ptx);

                // Return successful result
                var newAccountTx = new OrderedAccountTransactions(account, dateRange, newTx, bankTx.PendingTransactions);
                return new AccountTransactionImportSuccessResult(newAccountTx);
            }
            catch (Unauthorised ex)
            {
                return new AccountTransactionImportErrorResult(account, ex.Message);
            }
        }


        private static void SetTransactionOrder(CompletedAccountTxList newTx,
            CompletedAccountTxList repoTx)
        {
            var lastRepoTx = repoTx.Transactions.LastOrDefault();
            int dayCount = 0;
            string prevProcessedDate = string.Empty;
            if (lastRepoTx != null)
            {
                dayCount = lastRepoTx.TransactionOrder.Split('-')[1].Parse<int>();
                prevProcessedDate = lastRepoTx.ProcessedDate.ToString("yyyyMMdd");
            }
            SetTransactionOrderOnNewTransactions(dayCount, prevProcessedDate, newTx.Transactions);
        }

        private static void SetTransactionOrderOnNewTransactions(int dayTransactionCount,
            string lastTransactionProcessedDate,
            IList<CompletedTransaction> newTransactions)
        {
            var dayCount = dayTransactionCount;
            var prevProcessedDate = lastTransactionProcessedDate;

            foreach (var tx in newTransactions)
            {
                var currProcessedDate = tx.ProcessedDate.ToString("yyyyMMdd");
                if (prevProcessedDate != currProcessedDate)
                    dayCount = 1;
                else
                    dayCount++;
                tx.TransactionOrder = $"{currProcessedDate}-{dayCount.ToString("D3")}";
                prevProcessedDate = currProcessedDate;
            }
        }
    }
}