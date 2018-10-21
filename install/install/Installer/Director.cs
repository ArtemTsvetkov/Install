using install.Basic;
using install.Interfaces.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Installer
{
    class Director
    {
        private InstallBuilder installBuilder;

        public Director(InstallBuilder installBuilder)
        {
            this.installBuilder = installBuilder;
        }

        public void install(Config config)
        {
            installBuilder.addIniFiles(config.programPath);
            installBuilder.setLastDate(config.date);
            installBuilder.copyFileInDirrectory(config.programPath);
            if (config.avevasPath != null)
            {
                installBuilder.turnOnAveva(config.avevasPath, config.programPath);
            }
            if (config.logs != null)
            {
                installBuilder.addPathOfLogFiles(config.logs);
            }
            installBuilder.addConnectionString(config.connection);
            installBuilder.addParserLauncher(config.programPath);
            if (config.timeType.getType().Equals("HourType"))
            {
                installBuilder.createTaskForTasksManadger(
                    config.modificator, true, config.programPath);
            }
            else
            {
                installBuilder.createTaskForTasksManadger(
                    config.modificator, false, config.programPath);
            }
        }
    }
}
