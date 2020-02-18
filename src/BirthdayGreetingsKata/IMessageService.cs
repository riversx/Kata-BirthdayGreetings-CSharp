// using System.Net.Mail;

namespace BirthdayGreetings
{
    public interface IMessageService
    {
        void SendGreetingsToEmplyee(Employee employee);
    }
}