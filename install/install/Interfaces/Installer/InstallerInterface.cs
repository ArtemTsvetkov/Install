using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces.Installer
{
    interface InstallerInterface
    {
        void installAnalytics(string programsPath, string connectionString);
        void createDatabaseTables(string connectionString);
        void creatAdmin(string connectionString, string login, string password);
    }
}
