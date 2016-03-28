using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface ISettingRepository : IBaseRepository<Guid, Setting>
    {
        List<Setting> GetByName(string name);
        List<Setting> GetByPremiereDate(string date);
        List<Setting> GetByComposition(Composition composition);
    }
}
