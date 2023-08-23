﻿
namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Exceptions
{
    public class SecurityException : Exception
    {
        public SecurityException()
        { }

        public SecurityException(string message)
            : base(message)
        { }

        public SecurityException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
