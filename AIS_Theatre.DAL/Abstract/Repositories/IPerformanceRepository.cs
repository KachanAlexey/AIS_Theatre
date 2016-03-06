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
        IList<Performance> GetByDate(string date);
        IList<Performance> GetByTime(string time);
        IList<Performance> GetBySetting(Setting setting);
    }
}
