using System.Net;
using CleanCode.ExtractUntilYouDrop.BankTransactions.Entities;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions
{
    public class ImportAccount : Account
    {
        public new string BankName => Credential.Domain;
        public NetworkCredential Credential { get; }
        public string InternalBankId { get; set; }


        public ImportAccount(string accountNumber,
            string bankName,
            string username,
            string password,
            AccountType accountType)
            : base(accountNumber,
                bankName,
                accountType)
        {
            Credential = new NetworkCredential(username, password, bankName);
        }

        public ImportAccount(string accountNumber,
            string bankName,
            string username,
            string password,
            string internalBankId,
            AccountType accountType)
            : base(accountNumber,
                bankName,
                accountType)
        {
            Credential = new NetworkCredential(username, password, bankName);
            InternalBankId = internalBankId;
        }
    }
}
