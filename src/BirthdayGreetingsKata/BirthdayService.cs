// using System.Net.Mail;
using System.Collections.Generic;

namespace BirthdayGreetings
{
    public class BirthdayService
    {
        private readonly FileEmployeesRepository repository;
        private readonly SmtpMessageService messageService;

        public BirthdayService(FileEmployeesRepository repository, SmtpMessageService messageService)
        {
            this.repository = repository;
            this.messageService = messageService;
        }
        public void SendGreetings(XDate xDate)
        {
            List<Employee> employees = repository.GetAll();
            foreach (var employee in employees)
            {
                if (employee.IsBirthday(xDate))
                {
                    messageService.SendGreetingsToEmplyee(employee);
                }
            }
        }

    }
}