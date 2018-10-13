using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces.Installer
{
    interface InstallerInterface
    {
        void installParser(string programsPath, string connectionString,
            List<string> pathOfLogFile, int timeModificator,
            bool hourlyTimeModificator, string lastDateExist, string pathAvevaParser);
        void installAnalytics(string programsPath, string connectionString);
        void createDatabaseTables(string connectionString);
        void creatAdmin(string connectionString, string login, string password);
    }
}
