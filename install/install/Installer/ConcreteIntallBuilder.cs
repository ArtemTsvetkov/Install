using install.IniComponent;
using install.Interfaces.Installer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace install.Installer
{
    class ConcreteIntallBuilder : InstallBuilder
    {
        private Product product;

        public void addConnectionString(string connectionString)
        {
            product.INI.Write("Settings", "connectionString", connectionString);
        }

        public void addIniFiles(string programsPath)
        {
            product.INI = new IniFiles(programsPath + "\\config.ini");
        }

        public void addParserLauncher(string programsPath)
        {
            ReadWriteTextFile rwtf = new ReadWriteTextFile();
            List<string> buf = new List<string>();
            buf.Add(@"@echo off");
            buf.Add("cd /d " + programsPath);
            buf.Add(programsPath + "\\ServerKeyLogsParser.exe");
            rwtf.Write_to_file(buf, programsPath + "\\RunServerKeyLogsParser.bat", 0);
        }

        public void addPathOfLogFiles(List<string> pathOfLogFile)
        {
            for (int i = 0; i < pathOfLogFile.Count(); i++)
            {
                if (i == 0)
                {
                    product.INI.Write("Settings", "pathOfLogFile", pathOfLogFile.ElementAt(0));
                    product.INI.Write("Settings", "lastDateOfLogFile", product.lastRecordsTime);
                }
                else
                {
                    product.INI.Write("Settings", "pathOfLogFile" + i.ToString(),
                        pathOfLogFile.ElementAt(i));
                    product.INI.Write("Settings", "lastDateOfLogFile"
                            + i.ToString(), product.lastRecordsTime);
                }
            }
        }

        public void copyFileInDirrectory(string programsPath)
        {
            copyFile("ServerKeyLogsParser.exe",
                    Properties.Resources.ServerKeyLogsParser, programsPath);
        }

        public void createTaskForTasksManadger(int timeModificator, bool horly, string programsPath)
        {
            string command;
            if (!horly)
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

        public void reset()
        {
            product = new Product();
        }

        public void setLastDate(string lastDate)
        {
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
                string[] time = words[1].Split(new char[] { ':' }, 
                    StringSplitOptions.RemoveEmptyEntries);
                //удаление лишних нулей
                for (int h = 0; h < time.Count(); h++)
                {
                    string a = time[h];
                    if ((a.ElementAt(0).ToString().Equals("0")) & (a.Count() > 1))
                    {
                        time[h] = time[h].Remove(0, 1);
                    }
                }
                product.lastRecordsTime = date[0] + "." + date[1] + "." + date[2] + 
                    "_" + time[0] + ":" + time[1] + ":0";
            }
            else
            {
                string date = DateTime.Today.Day.ToString() + "." +
                    DateTime.Today.Month.ToString() + "." +
                    DateTime.Today.Year.ToString() +
                    "_1:1:1";
                product.lastRecordsTime = date;
            }
        }

        public void turnOnAveva(string pathAvevaParser, string programsPath)
        {
            product.INI.Write("Settings", "pathAvevasParser", pathAvevaParser);

            ReadWriteTextFile rwtf = new ReadWriteTextFile();
            List<string> buf = new List<string>();
            buf.Add(@"@echo off");
            buf.Add("cd /d " + pathAvevaParser);
            buf.Add("lsmon aveva > " + programsPath + "\\output.txt");
            rwtf.Write_to_file(buf, programsPath + "\\CreateAvevasLog.bat", 0);
        }

        private void copyFile(string fileNameWithType, byte[] fileFromResourses,
            string newFilesPath)
        {
            File.WriteAllBytes(@"" + newFilesPath + "\\" + fileNameWithType, fileFromResourses);
        }
    }
}
