using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IMusicianRepository : IBaseRepository<Guid, Musician>
    {
        List<Musician> GetByInstrument(string instrument);
        List<Musician> GetByBirthDate(string birthDate);
        List<Musician> GetBySalary(int salary);
        List<Musician> GetByExperience(string experience);
        List<Musician> GetMusiciansOfPerformance(Performance performance);
    }
}
