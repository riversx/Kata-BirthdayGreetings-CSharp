using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace BirthdayGreetings.Tests
{
    public class BirthdayServiceTests
    {

        [Test]
        public void SendGreetings_WithEmptyRepository_SendsZeroMessages()
        {
            // arrange
            IEmployeesRepository employeesRepository = Substitute.For<IEmployeesRepository>();
            employeesRepository.GetAll().Returns(new List<Employee>());
            IMessageService messageService = Substitute.For<IMessageService>();
            var birthdayService = new BirthdayService(employeesRepository, messageService);

            // act
            birthdayService.SendGreetings(new XDate("2020/02/18"));

            // assert
            messageService.DidNotReceiveWithAnyArgs().SendGreetingsToEmplyee(default);
        }
    }
}