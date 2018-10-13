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

        public void installParser(string programsPath, string connectionString, 
            List<string> pathOfLogFile, int timeModificator, 
            bool hourlyTimeModificator, string lastDate, string pathAvevaParser)
        {
            try
            {
                string last_record_s_time = "";

                IniFiles INI = new IniFiles(programsPath + "\\config.ini");

                //обновление даты последней записи в бд
                if (lastDate!=null)//если она существует
                {
                    string[] words = lastDate.Split(new char[] { ' ' }, 
                        StringSplitOptions.RemoveEmptyEntries);
                    string[] date = words[0].Split(new char[] { '.' },
                        StringSplitOptions.RemoveEmptyEntries);
                    //удаление лишних нулей
                    for (int i = 0; i < date.Count(); i++)
                    {
                        string a = date[i];
                        if ((a.ElementAt(0).ToString().Equals("0")) & (a.Count() > 1))
                        {
                            date[i] = date[i].Remove(0, 1);
                        }
                    }
                    string[] time = words[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    //удаление лишних нулей
                    for (int h = 0; h < time.Count(); h++)
                    {
                        string a = time[h];
                        if ((a.ElementAt(0).ToString().Equals("0")) & (a.Count() > 1))
                        {
                            time[h] = time[h].Remove(0, 1);
                        }
                    }
                    last_record_s_time = date[0] + "." + date[1] + "." + date[2] + "_" + time[0] + ":" + time[1] + ":0";
                }
                else
                {
                    string date = DateTime.Today.Day.ToString() + "." +
                        DateTime.Today.Month.ToString() + "." +
                        DateTime.Today.Year.ToString() +
                        "_1:1:1";
                    last_record_s_time = date;
                }

                //копирование файлов в новую дирректорию
                copyFile("ServerKeyLogsParser.exe",
                    Properties.Resources.ServerKeyLogsParser, programsPath);


                //создание файла настроек и файла запуска приложения
                if (pathAvevaParser != null)
                {
                    //buf.Add(textBox5.Text + " PathAvevasParser");
                    INI.Write("Settings", "pathAvevasParser", pathAvevaParser);
                }
                for (int i = 0; i < pathOfLogFile.Count(); i++)
                {
                    if (i == 0)
                    {
                        INI.Write("Settings", "pathOfLogFile", pathOfLogFile.ElementAt(0));
                        INI.Write("Settings", "lastDateOfLogFile", last_record_s_time);
                    }
                    else
                    {
                        INI.Write("Settings", "pathOfLogFile" + i.ToString(),
                            pathOfLogFile.ElementAt(i));
                        INI.Write("Settings", "lastDateOfLogFile"
                                + i.ToString(), last_record_s_time);
                    }
                }

                INI.Write("Settings", "connectionString", connectionString);

                ReadWriteTextFile rwtf = new ReadWriteTextFile();
                List<string> buf = new List<string>();
                buf.Add(@"@echo off");
                buf.Add("cd /d " + programsPath);
                buf.Add(programsPath + "\\ServerKeyLogsParser.exe");
                rwtf.Write_to_file(buf, programsPath + "\\RunServerKeyLogsParser.bat", 0);


                buf.Clear();
                buf.Add(@"@echo off");
                buf.Add("cd /d " + pathAvevaParser);
                buf.Add("lsmon aveva > " + programsPath + "\\output.txt");
                rwtf.Write_to_file(buf, programsPath + "\\CreateAvevasLog.bat", 0);


                //создание задания для планировщика заданий
                string command;
                if (!hourlyTimeModificator)
                {
                    command = @"/C SCHTASKS /Create /SC MINUTE /MO " + timeModificator + 
                        " /TR " + programsPath + 
                        "\\RunServerKeyLogsParser.bat /TN FlexLMParser";
                }
                else
                {
                    command = @"/C SCHTASKS /Create /SC HOURLY /MO " + timeModificator + 
                        " /TR " + programsPath + 
                        "\\RunServerKeyLogsParser.bat /TN FlexLMParser";
                }
                //в переменную check записываю значение только чтобы не создавать 
                //новую переменную, здесь просто лежит ответ командной строки
                WorkWithWindowsCommandLine wwwcl = new WorkWithWindowsCommandLine();
                string check = wwwcl.Run_command(command);
                if (check == "")
                {
                    string message = "Не удалось создать задание для планировщика заданий.";
                    string caption = "Ошибка";
                    DialogResult result;
                    result = MessageBox.Show(message, caption);
                }
            }
            catch (Exception ex)
            {
                ReadWriteTextFile rwtf = new ReadWriteTextFile();
                List<string> buf = new List<string>();
                buf.Add("-----------------------------------------------");
                buf.Add("Module: Form1");
                DateTime thisDay = DateTime.Now;
                buf.Add("Time: " + thisDay.ToString());
                buf.Add("Exception: " + ex.Message);

                rwtf.Write_to_file(buf, Directory.GetCurrentDirectory() + "\\Errors.txt", 0);
            }
        }

        private void copyFile(string fileNameWithType, byte[] fileFromResourses,
            string newFilesPath)
        {
            File.WriteAllBytes(@"" + newFilesPath + "\\" + fileNameWithType, fileFromResourses);
        }
    }
}
