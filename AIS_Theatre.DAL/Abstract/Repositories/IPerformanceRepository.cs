using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IPerformanceRepository : IBaseRepository<Guid, Performance>
    {
        List<Performance> GetByDate(string date);
        List<Performance> GetByTime(string time);
        List<Performance> GetBySetting(Setting setting);
        List<Performance> GetPerformancesOfActor(Actor actor);
        List<Performance> GetPerformancesOfMusician(Musician musician);
        List<Performance> GetPerformancesOfEmployee(Employee employee);
    }
}
