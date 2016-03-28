using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    internal class ConnectionStringBuilder
    {
        private string _dbms;

        internal ConnectionStringBuilder(string dbms)
        {
            this._dbms = dbms;
        }

        internal static string getConnectionString(string dbms)
        {
            switch (dbms)
            {
                case "PostgreSQL":
                    PostgreSQLConnectionStringBuilder postgreSQLConnectionStringBuilder = new PostgreSQLConnectionStringBuilder();
                    return postgreSQLConnectionStringBuilder.getConnectionString();
                default:
                    return null;
            }
        }
    }
}
