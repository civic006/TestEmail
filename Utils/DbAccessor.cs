using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEmailService.Contexts;
using TestEmailService.Models;

namespace ConsoleApplication1.Utils
{
    public static class DbAccessor
    {

        public static void InsertRecord(string name, string email) {
            //add user to db
            EmailContext ctx = new EmailContext();
            Contact cont = new Contact()
            {
                Name = name,
                Email = email
            };

            ctx.Contacts.Add(cont);
            ctx.SaveChanges();
        } }
    }
}
