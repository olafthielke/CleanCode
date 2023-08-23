namespace CleanCode.ExtractUntilYouDrop.UseCase.Exceptions
{
    public class MissingCustomerRegistration : InputDataException
    {
        public MissingCustomerRegistration()
            : base("Missing customer registration data.")
        { }
    }
}