namespace CleanCode.ExtractUntilYouDrop.UseCase.Exceptions
{
    public class ValidationException : InputDataException
    {
        public IList<string> Errors { get; } = new List<string>();
        public bool HasErrors => Errors.Any();
        public int ErrorCount => Errors.Count;

        public ValidationException()
        { }

        public ValidationException(IEnumerable<string> errors)
        {
            foreach (var error in errors)
                Errors.Add(error);
        }

        public void Add(string error)
        {
            Errors.Add(error);
        }
    }
}
