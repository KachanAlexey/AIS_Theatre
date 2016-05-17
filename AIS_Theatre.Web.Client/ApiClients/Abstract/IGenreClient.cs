using AIS_Theatre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIS_Theatre.Web.Client.ApiClients.Abstract
{
    public interface IGenreClient
    {
        Genre GetGenre(Guid id);

        void Create(Genre genre);

        List<Genre> GetAllGenres();

        void Edit(Genre genre);

        void Delete(Guid id);
    }
}