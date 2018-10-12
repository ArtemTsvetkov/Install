using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces.DataBase
{
    interface DataBaseControllerInterface
    {
        bool configAndCheckConnect(string connectionString);
        bool configAndExecute(string connectionString, string query);
    }
}
