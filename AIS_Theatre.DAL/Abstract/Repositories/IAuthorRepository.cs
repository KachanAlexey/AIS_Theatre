using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public interface IAuthorRepository : IBaseRepository<Guid, Author>
    {
        IList<Author> GetByGenre(Genre genre);
        IList<Author> GetByCountry(string country);
        IList<Author> GetAlive();
        IList<Author> GetDead();
    }
}
