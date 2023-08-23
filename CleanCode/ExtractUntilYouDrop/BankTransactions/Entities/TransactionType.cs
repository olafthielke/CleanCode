
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public enum TransactionType
    {
        Unknown = 0,
        TransferIn,
        TransferOut,
        Credit,
        Debit,
        DirectCredit,
        DirectDebit,
        EFTPOS,
        AutomaticPayment,
        Interest,
        BankFee,
        ATM,
    }
}
