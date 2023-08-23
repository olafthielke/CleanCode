
using CleanCode.ExtractUntilYouDrop.BankTransactions.Entities;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions
{
    public class DateRangeCalculator
    {
        private Date Today { get; }


        public DateRangeCalculator(Date today)
        {
            Today = today;
        }

        public DateRangeCalculator(DateTime today)
        {
            Today = new Date(today);
        }


        // Dates
        public Date Yesterday => Today.AddDays(-1);
        public Date StartOfThisWeek => CalcStartOfThisWeek();
        public Date StartOfLastWeek => StartOfThisWeek.AddDays(-7);
        public Date EndOfLastWeek => StartOfThisWeek.AddDays(-1);
        public Date StartOfThisMonth => CalcStartOfThisMonth();
        public Date StartOfLastMonth => CalcStartOfLastMonth();
        public Date EndOfLastMonth => StartOfThisMonth.AddDays(-1);

        // Date Ranges
        public DateRangeQuery TodayRange => new DateRangeQuery(Today, Today);
        public DateRangeQuery YesterdayRange => new DateRangeQuery(Yesterday, Yesterday);
        public DateRangeQuery ThisWeek => new DateRangeQuery(StartOfThisWeek, Today);
        public DateRangeQuery LastWeek => new DateRangeQuery(StartOfLastWeek, EndOfLastWeek);
        public DateRangeQuery ThisMonth => new DateRangeQuery(StartOfThisMonth, Today);
        public DateRangeQuery LastMonth => new DateRangeQuery(StartOfLastMonth, EndOfLastMonth);
        public DateRangeQuery LatestPeriod => CalcLatestDateRange();
        public DateRangeQuery CurrentPeriod => CalcCurrentDateRange();

        public DateRangeQuery CalcNamedDateRange(string namedPeriod)
        {
            if (namedPeriod == Constants.NamedPeriod.Today)
                return TodayRange;
            if (namedPeriod == Constants.NamedPeriod.Yesterday)
                return YesterdayRange;
            if (namedPeriod == Constants.NamedPeriod.ThisWeek)
                return ThisWeek;
            if (namedPeriod == Constants.NamedPeriod.LastWeek)
                return LastWeek;
            if (namedPeriod == Constants.NamedPeriod.ThisMonth)
                return ThisMonth;
            if (namedPeriod == Constants.NamedPeriod.LastMonth)
                return LastMonth;
            if (namedPeriod == Constants.NamedPeriod.Latest)
                return LatestPeriod;

            return null;
        }

        private Date CalcStartOfThisWeek()
        {
            var todayDayOfWeek = Today.DateTime.DayOfWeek;
            var daysOffsetToStartOfThisWeek = todayDayOfWeek - DayOfWeek.Monday;
            if (daysOffsetToStartOfThisWeek < 0) // ie. account for Sunday = 0
                daysOffsetToStartOfThisWeek = 6;
            return Today.AddDays(-daysOffsetToStartOfThisWeek);
        }

        private Date CalcStartOfThisMonth()
        {
            return new Date(new DateTime(Today.Year, Today.Month, 1));
        }

        private Date CalcStartOfLastMonth()
        {
            var year = EndOfLastMonth.Year;
            var month = EndOfLastMonth.Month;
            return new Date(new DateTime(year, month, 1));
        }

        private DateRangeQuery CalcLatestDateRange()
        {
            var currentMonth = Today.Month;
            var currentYear = Today.Year;

            var previousMonth = currentMonth == 1 ? 12 : currentMonth - 1;
            var previousMonthYear = previousMonth < 12 ? currentYear : currentYear - 1;
            var startOfPreviousMonth = new Date(new DateTime(previousMonthYear, previousMonth, 1));

            return new DateRangeQuery(startOfPreviousMonth, Today);
        }

        private DateRangeQuery CalcCurrentDateRange()
        {
            var startOfThisYear = new Date(new DateTime(Today.Year, 1, 1));
            return new DateRangeQuery(startOfThisYear, Today);
        }
    }
}

