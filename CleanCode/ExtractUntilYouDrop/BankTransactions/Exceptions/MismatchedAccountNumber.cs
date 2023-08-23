
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Exceptions
{
    public class MismatchedAccountNumber : Exception
    {
        public MismatchedAccountNumber(string expected, string actual)
            : base($"Expected Account Number '{expected}' but was '{actual}'.")
        { }
    }
}
