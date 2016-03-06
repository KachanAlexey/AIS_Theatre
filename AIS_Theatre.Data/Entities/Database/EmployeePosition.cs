using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class EmployeePosition : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public EmployeePosition Clone()
        {
            return (EmployeePosition)base.MemberwiseClone();
        }

        public EmployeePosition(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public EmployeePosition(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
