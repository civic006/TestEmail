using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using TestEmailService.Contexts;
using TestEmailService.Interfaces;
using System.Linq;
using ConsoleApplication1.Models;
using System.Net.Mime;

namespace TestEmailService.EmailObject
{
    public class BasicEmail
    {
        private MailAddress _from;
        private SmtpClient _client;
        private MailMessage _msg;
        private string _body;

        public BasicEmail(string fromEmail, string fromName, string pwd, SmtpClient client)
        {
            this._from = new MailAddress(fromEmail, fromName);
            this._body = @"Dear {0},<br><br>This month you had {1} accurate documentations out of {2}
                         total documents.<br><br>Thanks,<br><br>{3}<br><br><img src='cid:{4}'/>";
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
                    //Body = String.Format(this._body, data.AccurateDocs, data.TotalDocs),
                    IsBodyHtml = true
            };
                _msg.AlternateViews.Add(getEmbeddedImage(data));

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

                    //this._body = @"Dear {0},<br><br>This month you had {1} accurate documentations out of {2}
                    //     total documents.<br><br>Thanks,<br><br>{3}<br><br><img src='cid:{4}'/>";

        private AlternateView getEmbeddedImage(HospitalPerformance data)
        {
            string yourFile = "C:\\Users\\v-nathpa\\Documents\\seattlegen.png";
            LinkedResource inline = new LinkedResource(yourFile);
            inline.ContentId = Guid.NewGuid().ToString();
            string htmlBody = String.Format(this._body, this._msg.To, data.AccurateDocs, data.TotalDocs, _msg.From,inline.ContentId);
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);
            return alternateView;
        }

    }




}
