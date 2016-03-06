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
        IList<Composition> GetByGenre(Genre genre);
        IList<Composition> GetByAuthor(Author author);
        IList<Composition> GetByYear(string year);
        IList<Composition> GetByName(string name);
    }
}
