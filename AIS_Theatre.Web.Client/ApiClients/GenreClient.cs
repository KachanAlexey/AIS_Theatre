using AIS_Theatre.Web.Client.ApiClients.Abstract;
using AIS_Theatre.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIS_Theatre.Web.Client.ApiClients
{
    public class GenreClient : IGenreClient
    {
        private string _genreUri = "http://localhost:61258/api/genre";
        private RestClient _client = null;
        private string _token = ClientConfig.Token;

        public GenreClient()
        {
            _client = new RestClient(_genreUri);
            //AuthClient auth = new AuthClient();
            //_token = auth.Auth("Jaspe", "123");
        }

        /// <summary>
        /// get detailed information about Crash by id
        /// </summary>
        /// <param name="CrashId"></param>
        /// <returns></returns>
        public Genre GetGenre(Guid id)
        {
            var request = new RestRequest("details", Method.GET);
            request.AddParameter("id", id);
            AddAuthHeaders(request);
            IRestResponse<Genre> response = _client.Execute<Genre>(request);
            return response.Data;
        }

        public List<Genre> GetAllGenres()
        {
            var request = new RestRequest(Method.GET);
            AddAuthHeaders(request);
            IRestResponse<List<Genre>> response = _client.Execute<List<Genre>>(request);
            return response.Data;
        }

        public void Edit(Genre genre)
        {
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(genre);
            AddAuthHeaders(request);
            _client.Execute<Genre>(request);
        }

        public void Delete(Guid id)
        {
            var request = new RestRequest(Method.DELETE);
            request.AddParameter("id", id);
            AddAuthHeaders(request);
            _client.Execute<Genre>(request);
        }

        public void Create(Genre genre)
        {
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(genre);
            AddAuthHeaders(request);
            _client.Execute<Genre>(request);
        }

        private void AddAuthHeaders(RestRequest request)
        {
            request.AddHeader("Authorization", "Bearer " + _token);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
        }
    }
}