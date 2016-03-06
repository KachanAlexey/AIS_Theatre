using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IEmployeeInPerformanceRepository : IBaseRepository<Guid, EmployeeInPerformance>
    {
        IList<Performance> GetPerformancesOfEmployee(Employee employee);
        IList<Employee> GetEmployeesOfPerformance(Performance performance);
    }
}
