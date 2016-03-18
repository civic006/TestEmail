using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEmail.Models;

namespace ConsoleApplication1.Models
{
    public class HospitalPerformance
    {
        public int HospitalPerformanceId { get; set; }
        public int AccurateDocs { get; set; }
        public int TotalDocs { get; set; }
        public int ContactId { get; set; }
        public DateTime Date { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
