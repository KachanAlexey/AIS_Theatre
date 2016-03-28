using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IEmployeeRepository : IBaseRepository<Guid, Employee>
    {
        List<Employee> GetByPosition(EmployeePosition position);
        List<Employee> GetByBirthDate(string birthDate);
        List<Employee> GetBySalary(int salary);
        List<Employee> GetByExperience(string experience);
        List<Employee> GetEmployeesOfPerformance(Performance performance);
    }
}
