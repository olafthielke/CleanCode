using System.ComponentModel;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions
{
    public static class StringExtensions
    {
        public static T Parse<T>(this string value, T defaultValue = default(T))
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                return (T)converter.ConvertFromString(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T ParseOrThrow<T>(this string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                return (T)converter.ConvertFromString(value);
            }
            catch
            {
                throw new FormatException();
            }
        }

        public static string ParseIncludingDelimiters(this string input,
            string startDelimiter,
            string endDelimiter)
        {
            var startPos = input.IndexOf(startDelimiter);
            var inputFromStartPos = input.Substring(startPos);
            var endPos = inputFromStartPos.IndexOf(endDelimiter);
            return inputFromStartPos.Substring(0, endPos + endDelimiter.Length);
        }

        public static string ParseExcludingDelimiters(this string input,
            string startDelimiter,
            string endDelimiter)
        {
            var startPos = input.IndexOf(startDelimiter);
            var inputFromStartPos = input.Substring(startPos + startDelimiter.Length);
            var endPos = inputFromStartPos.IndexOf(endDelimiter);
            return inputFromStartPos.Substring(0, endPos);
        }

        public static string ReplaceWhitespaceWithSpaces(this string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"\s+", " ");
        }
    }
}
