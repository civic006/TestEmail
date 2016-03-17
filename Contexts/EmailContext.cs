using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TestEmailService.Models;
using ConsoleApplication1.Models;

namespace TestEmailService.Contexts
{
    public class EmailContext : DbContext
    {
        public EmailContext() : base()
        {
        }

        public DbSet<HospitalPerformance> HospitalPerformances { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
