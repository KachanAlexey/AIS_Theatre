using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IEmployeeRepository : IWorkerRepository
    {
        IList<Employee> GetByPosition(EmployeePosition position);
    }
}
