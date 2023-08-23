namespace CleanCode.ExtractUntilYouDrop.UseCase.Exceptions
{
    public abstract class InputDataException : Exception
    {
        protected InputDataException()
        { }

        protected InputDataException(string message)
            : base(message)
        { }
    }
}