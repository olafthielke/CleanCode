namespace CleanCode.ExtractUntilYouDrop.UseCase.Exceptions
{
    public class DuplicateCustomerEmailAddress : InputDataException
    {
        public string EmailAddress { get; }

        public DuplicateCustomerEmailAddress(string emailAddress)
            : base($"The email address '{emailAddress}' already exists in the system.")
        {
            EmailAddress = emailAddress;
        }
    }
}