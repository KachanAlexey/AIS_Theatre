using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class Employee : Worker
    {
        public EmployeePosition EmployeePosition { get; set; }

        public Employee(string fullName, string birthDate, int salary, string experience, EmployeePosition employeePosition)
        {
            this.Id = Guid.NewGuid();
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.Salary = salary;
            this.Experience = experience;
            this.EmployeePosition = employeePosition;
        }

        public Employee(Guid id, string fullName, string birthDate, int salary, string experience, EmployeePosition employeePosition)
        {
            this.Id = id;
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.Salary = salary;
            this.Experience = experience;
            this.EmployeePosition = employeePosition;
        }

        public Employee Clone()
        {
            return (Employee)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
