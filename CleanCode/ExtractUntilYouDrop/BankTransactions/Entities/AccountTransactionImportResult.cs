namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public abstract class AccountTransactionImportResult
    {
        public string BankName { get; }
        public string AccountNumber { get; }


        protected AccountTransactionImportResult(Account account)
        {
            BankName = account.BankName;
            AccountNumber = account.AccountNumber;
        }
    }
}