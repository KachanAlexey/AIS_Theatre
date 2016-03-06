using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IWorkerRepository : IBaseRepository<Guid, Worker>
    { 
        IList<Worker> GetByBirthDate(string birthDate);
        IList<Worker> GetBySalary(int salary);
        IList<Worker> GetByExperience(string experience);
        
    }
}
