// using System.Net.Mail;
using System.Collections.Generic;

namespace BirthdayGreetings
{
    public interface IEmployeesRepository
    {
        List<Employee> GetAll();
    }
}