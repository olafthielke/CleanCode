
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Exceptions
{
    public class MismatchedBankName : Exception
    {
        public MismatchedBankName(string expected, string actual)
            : base($"Expected Bank '{expected}' but was '{actual}'.")
        { }
    }
}
