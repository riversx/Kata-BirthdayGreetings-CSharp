using System.Linq;
using netDumbster.smtp;
using NUnit.Framework;

namespace BirthdayGreetings.Tests
{
    public class AcceptanceTest
    {
        const int NONSTANDARD_PORT = 3003;
        SimpleSmtpServer server;
        SmtpMessage[] emailIterator;

        [SetUp]
        public void SetUp()
        {
            server = SimpleSmtpServer.Start(NONSTANDARD_PORT);
        }

        [TearDown]
        public void TearDown()
        {
            server.Stop();
        }

        [Test]
        public void SendGreetings()
        {
            ExecuteBirthdayServiceFor(@"resources/employee_data.txt", "2008/10/08");
            AssertExpectedNumberOfEmailSentIs(1);
            AssertExpectedEmailWith_Subject_andBody_andSentTo("Happy Birthday!", "Happy Birthday, dear John!", "john.doe@foobar.com");
        }

        void AssertExpectedEmailWith_Subject_andBody_andSentTo(string subject, string body, string recipient)
        {
            var message = emailIterator.First();
            Assert.AreEqual(body, message.MessageParts[0].BodyData);
            Assert.AreEqual(subject, message.Headers["Subject"]);
            Assert.AreEqual(recipient, message.Headers["To"]);
        }

        void AssertExpectedNumberOfEmailSentIs(int expected)
        {
            Assert.AreEqual(expected, server.ReceivedEmail.Length);
        }

        void ExecuteBirthdayServiceFor(string employeeFileName, string date)
        {
            var service = new BirthdayService();
            service.SendGreetings(employeeFileName, new XDate(date), "localhost", NONSTANDARD_PORT);
            emailIterator = server.ReceivedEmail;
        }
    }
}