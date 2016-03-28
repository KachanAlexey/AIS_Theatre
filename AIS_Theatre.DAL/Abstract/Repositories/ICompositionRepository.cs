using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface ICompositionRepository : IBaseRepository<Guid, Composition>
    {
        List<Composition> GetByGenre(Genre genre);
        List<Composition> GetByAuthor(Author author);
        List<Composition> GetByYear(string year);
        List<Composition> GetByName(string name);
    }
}
