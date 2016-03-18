using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEmail.Contexts;
using TestEmail.Models;

namespace ConsoleApplication1.Utils
{
    public static class DbAccessor
    {

        public static void InsertContactRecord(string name, string email) {
            //add user to db
            EmailContext ctx = new EmailContext();
            Contact cont = new Contact()
            {
                Name = name,
                Email = email
            };

            ctx.Contacts.Add(cont);
            ctx.SaveChanges();
            ctx.Dispose();
        }

        public static void InsertHospitalData(int accurateDocs, int totalDocs, int contactID, DateTime date)
        {
            //add user to db
            EmailContext ctx = new EmailContext();
            HospitalPerformance cont = new HospitalPerformance()
            {
                AccurateDocs = accurateDocs,
                TotalDocs = totalDocs,
                Date = DateTime.Now,
                ContactId = contactID
            };

            ctx.HospitalPerformances.Add(cont);
            ctx.SaveChanges();
            ctx.Dispose();
        }
    }
}
