namespace CleanCode.ExtractUntilYouDrop.UseCase.Entities
{
    public class CustomerRegistration
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }


        public CustomerRegistration(string firstName, string lastName, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}