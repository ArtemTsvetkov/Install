using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces.Installer
{
    interface InstallBuilder
    {
        void setLastDate(string lastDate);
        void addIniFiles(string programsPath);
        void copyFileInDirrectory(string programsPath);
        void turnOnAveva(string pathAvevaParser, string programsPath);
        void addPathOfLogFiles(List<string> pathOfLogFile);
        void addConnectionString(string connectionString);
        void addParserLauncher(string programsPath);
        void createTaskForTasksManadger(int timeModificator, bool horly, string programsPath);
        void reset();
    }
}
