
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Interfaces
{
    public interface ILocalTimeProvider
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}
