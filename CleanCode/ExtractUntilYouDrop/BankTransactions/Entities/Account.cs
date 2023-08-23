namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class Account
    {
        public string AccountNumber { get; }
        public string BankName { get; }
        public AccountType AccountType { get; }

        public bool IsTransaction => AccountType == AccountType.Transaction;
        public bool IsCreditCard => AccountType == AccountType.CreditCard;

        public Account(string accountNumber,
            string bankName,
            AccountType accountType)
        {
            AccountNumber = accountNumber;
            BankName = bankName;
            AccountType = accountType;
        }
    }
}
