using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using TestEmailService.Contexts;
using TestEmailService.Interfaces;
using System.Linq;
using ConsoleApplication1.Models;

namespace TestEmailService.EmailObject
{
    public class DataEmail
    {
        private MailAddress _from;
        private SmtpClient _client;
        private MailMessage _msg;
        private String _body;

        public DataEmail(string fromEmail, string fromName, string pwd, SmtpClient client, string body)
        {
            this._from = new MailAddress(fromEmail, fromName);
            this._body = "This month you had {0} accurate documentations out of {1} total documents.";
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
                    Subject = toAddress.DisplayName + "Testing",
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
            EmailContext ctx = new EmailContext();

            var emailBodyData = from c in ctx.Contacts
                                from d in c.HospitalPerfoamce
                                where c.Email == "knguyen07@gmail.com"
                                orderby d.Date descending
                                select d;



            HospitalPerformance data = emailBodyData.First();
            try
            {
                _msg = new MailMessage(_from, toAddress)
                {
                    Subject = toAddress.DisplayName + "Testing",
                    Body = String.Format(this._body, data.AccurateDocs, data.TotalDocs)

                };
                Console.WriteLine("{0} :: Sending async", toAddress.DisplayName);
                _client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                _client.SendAsync(_msg, toAddress.DisplayName);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                Console.WriteLine(ex.ToString());
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
