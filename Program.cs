using ConsoleApplication1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TestEmailService.Configuration;
using TestEmailService.Contexts;
using TestEmailService.EmailObject;
using TestEmailService.Interfaces;
using TestEmailService.Models;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            sendGmail();
        }

        public static void sendLocal()
        {
            EmailContext db = new EmailContext();
            List<Contact> allContacts = db.Contacts.ToList();

            IConfig reader = new AppConfigReader();
            //NetworkCredential cred = new NetworkCredential(reader.GetUserName(), reader.GetPassword());


            foreach (Contact cont in allContacts)
            {
                SmtpClient client = new SmtpClient();
                //{
                //    Credentials = cred,   // Send our account login details to the client.
                //    EnableSsl = true
                //};

                DataEmail dataEmail = new DataEmail(reader.GetUserName(), reader.GetName(), reader.GetPassword(), client, "hi hi hi");
                // Send our email
                dataEmail.SendAsync(cont.Email, cont.Name);

            }
            Console.Read();
        }

        public static void sendGmail()
        {
            EmailContext db = new EmailContext();
            List<Contact> allContacts = db.Contacts.ToList();

            IConfig reader = new AppConfigReader();
            NetworkCredential cred = new NetworkCredential(reader.GetUserName(), reader.GetPassword());


            foreach (Contact cont in allContacts)
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = cred,   // Send our account login details to the client.
                    EnableSsl = true
                };

                DataEmail dataEmail = new DataEmail(reader.GetUserName(), reader.GetName(), reader.GetPassword(), client, "hi hi hi");
                // Send our email
                dataEmail.SendAsync(cont.Email, cont.Name);

            }
            Console.Read();
        }
    }
}
