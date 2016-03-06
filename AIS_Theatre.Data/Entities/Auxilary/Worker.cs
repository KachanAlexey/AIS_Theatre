using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Worker : BaseEntity<Guid>
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public int Salary { get; set; }
        public string Experience { get; set; }
    }
}
