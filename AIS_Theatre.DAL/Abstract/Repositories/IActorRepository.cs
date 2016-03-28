using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IActorRepository : IBaseRepository<Guid, Actor>
    {
        List<Actor> GetByFeatures(ActorsFeatures features);
        List<Actor> GetByBirthDate(string birthDate);
        List<Actor> GetBySalary(int salary);
        List<Actor> GetByExperience(string experience);
        List<Actor> GetActorsOfPerformance(Performance performance);
    }
}
