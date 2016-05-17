using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIS_Theatre.Web.Client.ApiClients
{
    public class AuthClient
    {
        public string Auth(string username, string password)
        {
            RestClient client = new RestClient("http://localhost:61258");
            var request = new RestRequest("/token", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddParameter("username", username);
            request.AddParameter("grant_type", "password");
            request.AddParameter("password", password);
            IRestResponse response = client.Execute(request);
            var tokenString = response.Content;
            JToken token = JToken.Parse(tokenString);
            return token["access_token"].ToString();
        }

        public void Register(string username, string password)
        {
            RestClient client = new RestClient("http://localhost:61258/api/Account/Register");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            IRestResponse response = client.Execute(request);
        }
    }
}