using System.Collections.Generic;
using System.Linq;

namespace BirthdayGreetings
{
    public class BirthdayService
    {
        private readonly FileEmployeesRepository repository;
        private readonly IMessageService messageService;

        public BirthdayService(FileEmployeesRepository repository, IMessageService messageService)
        {
            this.repository = repository;
            this.messageService = messageService;
        }
        public void SendGreetings(XDate xDate)
        {
            List<Employee> employees = repository.GetAll()
                .Where(e => e.IsBirthday(xDate))
                .ToList();
            employees.AsParallel()
                .ForAll(employee => messageService.SendGreetingsToEmplyee(employee));
        }

    }
}