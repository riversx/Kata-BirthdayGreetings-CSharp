using System.Net.Mail;

namespace BirthdayGreetings
{
	public class BirthdayService
	{
		public void SendGreetings(string fileName, XDate xDate, string smtpHost, int smtpPort)
		{
			if (System.IO.File.Exists(fileName))
			{
				var objReader = new System.IO.StreamReader(fileName);
				do
				{
					var textLine = objReader.ReadLine();
					if (!string.IsNullOrEmpty(textLine) && !textLine.Contains("last_name"))
					{
						var employeeData = textLine.Split(new char[] { ',' });
						var employee = new Employee
						{
							FirstName = employeeData[1],
							LastName = employeeData[0],
							BirthDate = new XDate(employeeData[2]),
							Email = employeeData[3]
						};
						if (employee.IsBirthday(xDate))
						{
							var recipient = employee.Email;
							var body = string.Format("Happy Birthday, dear {0}!", employee.FirstName);
							var subject = "Happy Birthday!";
							SendMessage(smtpHost, smtpPort, "sender@here.com", subject, body, recipient);
						}
					}
				} while (objReader.Peek() != -1);
			}

		}

		void SendMessage(string smtpHost, int smtpPort, string sender, string subject, string body, string recipient)
		{
			// Create a mail session
			//java.util.Properties props = new java.util.Properties();
			//props.put("mail.smtp.host", smtpHost);
			//props.put("mail.smtp.port", "" + smtpPort);

			// Construct the message
			var msg = new MailMessage(sender, recipient, subject, body);
			// Send the message
			SendMessage(smtpHost, smtpPort, msg);

		}

		// made protected for testing :-(
		protected void SendMessage(string smtpHost, int smtpPort, MailMessage mail)
		{
			var smtpClient = new SmtpClient(smtpHost, smtpPort);
			smtpClient.Credentials = new System.Net.NetworkCredential("info@MyWebsiteDomainName.com", "myIDPassword");
			smtpClient.UseDefaultCredentials = false;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.EnableSsl = false;
			smtpClient.Send(mail);
		}
	}
}