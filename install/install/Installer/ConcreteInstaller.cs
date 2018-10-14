using install.Hash;
using install.IniComponent;
using install.Interfaces;
using install.Interfaces.DataBase;
using install.Interfaces.Installer;
using install.Interfaces.QueryConfigurator;
using install.QueryConfigurator;
using install.Security;
using install.WorkWithDataBase.MsSqlServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace install.Installer
{
    class ConcreteInstaller : InstallerInterface
    {
        private QueryConfiguratorInterface queryConfigurator;
        private DataBaseControllerInterface dataBaseController;

        public ConcreteInstaller()
        {
            queryConfigurator = new MsSqlQueryConfigurator();
            dataBaseController = new MsSqlServerController();
        }

        public void creatAdmin(string connectionString, string login, string password)
        {
            HashWorkerInterface<HashConfig> hashWorker = new HashWorker();
            SecurityUserInterface user = new SecurityUser(login, password);
            user.setAdmin(true);
            string sult = hashWorker.getSult(user);
            dataBaseController.configAndExecute(connectionString, 
                queryConfigurator.addAdmin(user.getLogin(), 
                hashWorker.getHash(user.getPassword(),sult),sult));
        }

        public void createDatabaseTables(string connectionString)
        {
            List<string> querys = new List<string>();
            querys.Add(queryConfigurator.createTableUsers());
            querys.Add(queryConfigurator.createTableVendor());
            querys.Add(queryConfigurator.createTableSoftware());
            querys.Add(queryConfigurator.createTableHistory());
            querys.Add(queryConfigurator.createTableUST());

            for(int i=0; i<querys.Count(); i++)
            {
                dataBaseController.configAndExecute(connectionString, querys.ElementAt(i));
            }
        }

        public void installAnalytics(string programsPath, string connectionString)
        {
            //копирование файлов в новую директорию
            IniFiles INI = new IniFiles(programsPath + "\\config.ini");
            INI.Write("Settings", "connectionString", connectionString);
            copyFile("Analytics.exe", Properties.Resources.Analytics, programsPath);
        }

        private void copyFile(string fileNameWithType, byte[] fileFromResourses,
            string newFilesPath)
        {
            File.WriteAllBytes(@"" + newFilesPath + "\\" + fileNameWithType, fileFromResourses);
        }
    }
}
