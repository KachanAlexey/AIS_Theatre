using AIS_Theatre.Data;
using AIS_Theatre.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AIS_Theatre.Web.API.Controllers.EntitiesControllers
{
    public class GenreController : ApiController
    {
        private IGenreRepository _repository = new UnitOfWork().GenreRepository;

        // GET api/crash
        [Authorize]
        public List<Genre> Get()
        {
            return _repository.GetAll();
        }

        // GET api/crash/id
        [Authorize]
        public Genre Get(Guid id)
        {
            return _repository.GetById(id);
        }

        // POST api/crash
        [Authorize]
        public void Post([FromBody]Genre value)
        {
            _repository.Upsert(value);
        }

        // DELETE api/crash/id
        [Authorize]
        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}