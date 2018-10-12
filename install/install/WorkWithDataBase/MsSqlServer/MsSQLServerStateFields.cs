using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.WorkWithDataBase.MsSqlServer
{
    class MsSQLServerStateFields
    {
        private string connectionString;
        private List<string> query;

        public MsSQLServerStateFields(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MsSQLServerStateFields(string connectionString, List<string> query)
        {
            this.connectionString = connectionString;
            this.query = query;
        }

        public string getConnectionString()
        {
            return connectionString;
        }

        public List<string> getQuery()
        {
            if (query == null)
            {
                return null;
            }
            else
            {
                return query;
            }
        }
    }
}