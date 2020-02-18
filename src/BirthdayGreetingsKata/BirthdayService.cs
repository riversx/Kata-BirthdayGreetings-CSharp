// using System.Net.Mail;
using System.Collections.Generic;

namespace BirthdayGreetings
{
    public class BirthdayService
    {
        public void SendGreetings(string fileName, XDate xDate, string smtpHost, int smtpPort)
        {
            if (System.IO.File.Exists(fileName))
            {
                var repositoy = new FileEmployeesRepository(fileName);
                List<Employee> employees = repositoy.GetAll();

                var messageService = new SmtpMessageService();
                foreach (var employee in employees)
                {
                    if (employee.IsBirthday(xDate))
                    {
                        messageService.SendGreetingsToEmplyee(smtpHost, smtpPort, employee);
                    }
                }
            }

        }

    }
}