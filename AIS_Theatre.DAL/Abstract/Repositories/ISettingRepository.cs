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
        IList<Setting> GetByName(string name);
        IList<Setting> GetByPremiereDate(string date);
        IList<Setting> GetByComposition(Composition composition);
    }
}
