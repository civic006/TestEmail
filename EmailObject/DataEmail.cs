using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using TestEmailService.Interfaces;

namespace TestEmailService.EmailObject
{
    public class DataEmail
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
        private void Send(MailAddress toAddress)
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

        //send email asyn given strings of email and name
        public void SendAsync(string toEmail, string toName)
        {
            SendAsync(new MailAddress(toEmail, toName));
        }

        //send email async given a mail service
        private void SendAsync(MailAddress toAddress)
        {
            try
            {
                _msg = new MailMessage(_from, toAddress)
                {
                    Subject = this._subject,
                    Body = this._body

                };
                Console.WriteLine("{0} :: Sending async", toAddress.DisplayName);
                _client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                _client.SendAsync(_msg, toAddress.DisplayName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
                
            }
            else
            {
                Console.WriteLine("{0} :: Message sent.", token);
            }
            _msg.Dispose();
            _client.Dispose();
        }

    }


}
