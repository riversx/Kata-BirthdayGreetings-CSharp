using System;
using System.Globalization;

namespace BirthdayGreetings
{
    public class XDate
    {
		readonly DateTime dateTime;

		public int Day
        {
            get { return dateTime.Day; } 
        }

        public int Month
        {
            get { return dateTime.Month; }
        }

        public int Year
        {
            get { return dateTime.Year; }
        }

        public XDate(string d):this(DateTime.ParseExact(d, "yyyy/MM/dd", null, DateTimeStyles.None)) {}

        public XDate(DateTime d)
        {
			dateTime = d;
        }

        public bool IsSameDay(object anotherDate)
        {
            if (anotherDate == null) return false;
            if (!(anotherDate is XDate)) return false;
            var date = (XDate) anotherDate;
            return dateTime.Day == date.Day && dateTime.Month == date.Month;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is XDate)) return false;
            var _date = (XDate)obj;
            return _date.IsSameDay(this) && _date.Year == this.Year;
        }

		public override int GetHashCode()
		{
			int result = 17;
			result = 31 * result + dateTime.Day.GetHashCode();
			result = 31 * result + dateTime.Month.GetHashCode();
			result = 31 * result + dateTime.Year.GetHashCode();
			return result;
		}

    }
}