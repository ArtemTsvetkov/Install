using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces.QueryConfigurator
{
    interface QueryConfiguratorInterface
    {
        string createTableUsers();
        string createTableVendor();
        string createTableSoftware();
        string createTableHistory();
        string createTableUST();
        string addAdmin(string login, string password, string sult);
        string checkUstTable();
    }
}
