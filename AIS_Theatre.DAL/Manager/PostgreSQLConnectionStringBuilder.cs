using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    internal class PostgreSQLConnectionStringBuilder
    {
        private string _server = "localhost";
        private string _port = "5432";
        private string _user = "postgres";
        private string _password = "Masterkey";
        private string _database = "theatre_db";

        private string _connectionString = "";

        private NpgsqlConnectionStringBuilder postrgreSQLStringBuilder = new NpgsqlConnectionStringBuilder();

        internal PostgreSQLConnectionStringBuilder()
        {
            postrgreSQLStringBuilder.Add("Server", _server);
            postrgreSQLStringBuilder.Add("Port", _port);
            postrgreSQLStringBuilder.Add("User Id", _user);
            postrgreSQLStringBuilder.Add("Password", _password);
            postrgreSQLStringBuilder.Add("Database", _database);
        }

        internal string getConnectionString()
        {
            _connectionString = postrgreSQLStringBuilder.ConnectionString;
            return _connectionString;
        }
    }
}
