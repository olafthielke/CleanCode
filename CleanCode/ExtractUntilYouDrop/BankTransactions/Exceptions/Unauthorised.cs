
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Exceptions
{
    public class Unauthorised : SecurityException
    {
        private const string UnauthorisedMessage = "Unauthorised";

        public Unauthorised()
            : base(UnauthorisedMessage)
        { }

        public Unauthorised(Exception innerException)
            : base(UnauthorisedMessage, innerException)
        { }
    }
}
