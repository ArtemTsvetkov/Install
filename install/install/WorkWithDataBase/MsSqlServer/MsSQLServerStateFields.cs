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

        public MsSQLServerStateFields(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string getConnectionString()
        {
            return connectionString;
        }
    }
}