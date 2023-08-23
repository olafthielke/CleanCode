using System.Net;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Interfaces
{
    public interface IBankTransactionClientFactory
    {
        Task<IBankTransactionClient> GetLoggedInClient(NetworkCredential credential, bool isLogged = false);
    }
}
