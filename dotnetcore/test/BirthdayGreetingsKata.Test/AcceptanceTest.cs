using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BirthdayGreetings.Tests
{
	[TestClass]
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

		[TestMethod]
		public void SendGreetings()
		{
			StartBirthdayServiceFor(@"resources/employee_data.txt", "2008/10/08");
			ExpectNumberOfEmailSentIs(1);
			ExpectEmailWithSubject_andBody_sentTo("Happy Birthday!", "Happy Birthday, dear John!", "john.doe@foobar.com");
		}

		void ExpectEmailWithSubject_andBody_sentTo(string subject, string body, string recipient)
		{
			var message = emailIterator.First();
			Assert.AreEqual(body, message.Body.Trim());
			Assert.AreEqual(subject, message.Headers["Subject"]);
			Assert.AreEqual(recipient, message.Headers["To"]);
		}

		void ExpectNumberOfEmailSentIs(int expected)
		{
			Assert.AreEqual(expected, server.ReceivedEmail.Length);
		}

		void StartBirthdayServiceFor(string employeeFileName, string date)
		{
			var service = new BirthdayService();
			service.SendGreetings(employeeFileName, new XDate(date), "localhost", NONSTANDARD_PORT);
			emailIterator = server.ReceivedEmail;
		}
	}
}