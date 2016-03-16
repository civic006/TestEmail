using System;
using System.Net;
using System.Net.Mail;
using TestEmailService.Interfaces;

namespace TestEmailService.EmailObject
{
    public class DataEmail : IEmail
    {
        private MailAddress _from;
        private SmtpClient _client;
        private MailMessage _msg;
        private String _body;
        private String _subject;

        public DataEmail(string fromEmail, string fromName, string pwd, SmtpClient client, string body)
        {
            this._from = new MailAddress(fromEmail, fromName);
            this._subject = "Tester";
            this._body = body;
            this._client = client;
        }

        //send email given strings of email and name
        public void Send(string toEmail, string toName)
        {
            Send(new MailAddress(toEmail, toName));
        }

        //send email given a mail service
        public void Send(MailAddress toAddress)
        {
            try
            {
                _msg = new MailMessage(_from, toAddress)
                {
                    Subject = this._subject,
                    Body = this._body

                };
                _client.Send(_msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            _msg.Dispose();
        }
    }
}
