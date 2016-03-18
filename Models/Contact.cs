using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEmail.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public virtual List<HospitalPerformance> HospitalPerfoamce { get; set; }

    }
}
