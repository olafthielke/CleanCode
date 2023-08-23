
using System.Net;
using CleanCode.ExtractUntilYouDrop.BankTransactions.Entities;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Interfaces
{
    public interface IBankTransactionClient : IBankName
    {
        Task Login(NetworkCredential credential);
        Task<OrderedAccountTransactions> GetTransactions(ImportAccount account,
            DateRangeQuery dateRange);
    }
}
