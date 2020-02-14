namespace BirthdayGreetings
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public XDate BirthDate { get; set; }

        public bool IsBirthday(XDate anotherDay)
        {
            return BirthDate.IsSameDay(anotherDay);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Employee)) return false;
            var emp = (Employee) obj;
            return emp.FirstName.Equals(FirstName)
                   && emp.LastName.Equals(LastName)
                   && emp.Email.Equals(Email)
                   && emp.BirthDate.Equals(BirthDate);
        }

		public override int GetHashCode()
		{
			int result = 17;
			result = 31 * result + FirstName.GetHashCode();
			result = 31 * result + LastName.GetHashCode();
			result = 31 * result + Email.GetHashCode();
			result = 31 * result + BirthDate.GetHashCode();
			return result;
		}

    }
}