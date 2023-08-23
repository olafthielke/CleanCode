using System.Globalization;

namespace CleanCode.ExtractUntilYouDrop.BankTransactions.Entities
{
    public class Date : IComparable
    {
        public DateTime DateTime { get; private set; }

        public int Day => DateTime.Day;
        public int Month => DateTime.Month;
        public int Year => DateTime.Year;

        public Date(string date)
        {
            DateTime = date.ParseOrThrow<DateTime>();
        }

        public Date(DateTime dt)
        {
            DateTime = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        public Date(Date date)
        {
            DateTime = date.DateTime;
        }

        public static Date NzToday()
        {
            return new Date(DateTime.Now.Date);
        }

        public int CompareTo(object obj)
        {
            if ((Date)obj < this)
                return 1;
            if ((Date)obj > this)
                return -1;
            return 0;
        }

        public void MoveNext()
        {
            DateTime = DateTime.AddDays(1);
        }

        public Date AddDays(int days)
        {
            return new Date(DateTime.AddDays(days));
        }


        public override string ToString()
        {
            return DateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string ToString(string format)
        {
            return DateTime.ToString(format, CultureInfo.InvariantCulture);
        }

        public static bool operator ==(Date date1, Date date2)
        {
            var isDate1Null = ReferenceEquals(date1, null);
            var isDate2Null = ReferenceEquals(date2, null);
            if (isDate1Null && isDate2Null)
                return true;
            else if (isDate1Null || isDate2Null)
                return false;
            return date1.DateTime == date2.DateTime;
        }

        public static bool operator !=(Date date1, Date date2)
        {
            var isDate1Null = ReferenceEquals(date1, null);
            var isDate2Null = ReferenceEquals(date2, null);
            if (isDate1Null && isDate2Null)
                return false;
            else if (isDate1Null || isDate2Null)
                return true;
            return date1.DateTime != date2.DateTime;
        }

        public static bool operator >=(Date date1, Date date2)
        {
            return date1.DateTime >= date2.DateTime;
        }

        public static bool operator >(Date date1, Date date2)
        {
            return date1.DateTime > date2.DateTime;
        }

        public static bool operator <=(Date date1, Date date2)
        {
            return date1.DateTime <= date2.DateTime;
        }

        public static bool operator <(Date date1, Date date2)
        {
            return date1.DateTime < date2.DateTime;
        }

        public override bool Equals(object o)
        {
            if (o is Date)
                return (Date)o == this;
            return false;
        }

        public override int GetHashCode() => GetHashCode();
    }
}
