using System;
using System.Collections.Generic;
using System.Linq;
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
            EmailContext db = new EmailContext();
            List<Contact> allContacts = db.Contacts.ToList();

            IConfig reader = new AppConfigReader();
            IEmail dataEmail = new DataEmail(reader.GetUserName(), reader.GetName(), reader.GetPassword());
            foreach (Contact cont in allContacts)
            {
                // Send our email
                dataEmail.Send(cont.Email,cont.Name);    

            }
            
        }
    }
}
