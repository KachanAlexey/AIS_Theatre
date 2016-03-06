using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IMusicianInPerformanceRepository : IBaseRepository<Guid, MusicianInPerformance>
    {
        IList<Performance> GetPerformancesOfMusician(Musician musician);
        IList<Musician> GetMusiciansOfPerformance(Performance performance);
    }
}
