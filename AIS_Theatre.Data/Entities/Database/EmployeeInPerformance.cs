using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class EmployeeInPerformance : BaseEntity<Guid>
    {
        public Employee Employee { get; set; }
        public Performance Performance { get; set; }

        public EmployeeInPerformance(Employee employee, Performance performance)
        {
            this.Id = Guid.NewGuid();
            this.Employee = employee;
            this.Performance = performance;
        }

        public EmployeeInPerformance(Guid id, Employee employee, Performance performance)
        {
            this.Id = id;
            this.Employee = employee;
            this.Performance = performance;
        }

        public EmployeeInPerformance Clone()
        {
            return (EmployeeInPerformance)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("{0} in {1}", Employee.ToString(), Performance.ToString());
        }
    }
}
