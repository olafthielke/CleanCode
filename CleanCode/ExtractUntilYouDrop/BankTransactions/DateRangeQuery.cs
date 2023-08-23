
using CleanCode.ExtractUntilYouDrop.BankTransactions.Entities;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions
{
    public class DateRangeQuery
    {
        public Date StartDate { get; }
        public Date EndDate { get; }

        public DateRangeQuery(string startDate, string endDate)
            : this(new Date(startDate), new Date(endDate))
        { }

        public DateRangeQuery(DateTime startDate, DateTime endDate)
            : this(new Date(startDate), new Date(endDate))
        { }

        public DateRangeQuery(Date startDate, Date endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
