// using System.Net.Mail;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;

namespace BirthdayGreetings
{
    public class BirthdayService
    {
        public void SendGreetings(string fileName, XDate xDate, string smtpHost, int smtpPort)
        {
            if (System.IO.File.Exists(fileName))
            {
                var objStream = new FileStream(fileName, FileMode.Open);
                var objReader = new StreamReader(objStream);
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
                            var subject = "Happy Birthday";
                            SendMessage(smtpHost, smtpPort, "sender@here.com", subject, body, recipient);
                        }
                    }
                } while (objReader.Peek() != -1);
            }

        }

        void SendMessage(string smtpHost, int smtpPort, string sender, string subject, string body, string recipient)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(sender));
            message.To.Add(new MailboxAddress(recipient));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };
            // Send the message
            SendMessage(smtpHost, smtpPort, message);
        }

        // made protected for testing :-( 
        // this was originally needed in java  - check if is needed here as well.
        protected void SendMessage(string smtpHost, int smtpPort, MimeMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(smtpHost, smtpPort, false);
                // smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                // // Note: since we don't have an OAuth2 token, disable 	
                // // the XOAUTH2 authentication mechanism.     
                // smtpClient.Authenticate("info@MyWebsiteDomainName.com", "myIDPassword");
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
        }
    }
}