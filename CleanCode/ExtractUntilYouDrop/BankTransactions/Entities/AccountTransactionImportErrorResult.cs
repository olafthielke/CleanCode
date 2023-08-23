
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class AccountTransactionImportErrorResult : AccountTransactionImportResult
    {
        public string ErrorMessage { get; }


        public AccountTransactionImportErrorResult(Account account, string errorMessage)
            : base(account)
        {
            ErrorMessage = errorMessage;
        }
    }
}
