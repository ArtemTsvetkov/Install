using install.ExceptionHandler.Concrete;
using install.ExceptionHandler.Interfaces;
using install.ExceptionHandler.View.Information.PopupWindow;
using install.Exceptions;
using install.IniComponent;
using install.Interfaces.Data;
using install.WorkWithDataBase.MsSqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace install
{
    public partial class Form1 : Form
    {
        private bool installParser = true;

        public Form1()
        {
            InitializeComponent();
            //
            //Exception handler
            //
            ConcreteExceptionHandlerInitializer.initThisExceptionHandler(
                ExceptionHandler.Concrete.ExceptionHandler.getInstance());
            //настройка переключателя
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabStop = false;
            tabControl1.SelectedIndex = 7;
            radioButton1.Checked = true;
            textBox5.Visible = false;
            label25.Visible = false;
            button24.Visible = false;
            radioButton3.Checked = true;
            checkBox2.Checked = true;
            //добавление колонок в таблицу
            DataGridViewTextBoxColumn coefficient0;
            coefficient0 = new DataGridViewTextBoxColumn();
            coefficient0.Width = 363;
            coefficient0.HeaderText = "Пути к логам";
            dataGridView1.Columns.Add(coefficient0);
        }

        private bool configProxyForLoadDataFromBDAndExecute(string connectionString)
        {
            try
            {
                DataWorker<MsSQLServerStateFields, DataSet> accessProxy = new MsSQLServerProxy();
                MsSQLServerStateFields configProxy =
                    new MsSQLServerStateFields(connectionString);
                accessProxy.setConfig(configProxy);
                accessProxy.connect();
                ExceptionViewInterface<InformationPopupWindowConfig> view = 
                    new InformationPopupWindow();
                InformationPopupWindowConfig config = new InformationPopupWindowConfig(
                    "Соединение установлено");
                view.setConfig(config);
                view.show();
                return true;
            }
            catch(Exception ex)
            {
                ExceptionHandler.Concrete.ExceptionHandler.getInstance().processing(
                    new NoDataBaseConnection());
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                bool no_errors = true;
                for (int i=0;i<textBox1.Text.Count();i++)
                {
                    if (((textBox1.Text.ElementAt(i) >= 'а') && (textBox1.Text.ElementAt(i) <= 'я')) || ((textBox1.Text.ElementAt(i) >= 'А') && (textBox1.Text.ElementAt(i) <= 'Я')))
                    {
                        no_errors = false;
                    }
                    if(textBox1.Text.ElementAt(i) == ' ')
                    {
                        no_errors = false;
                    }
                }
                if (no_errors == true)
                {
                    tabControl1.SelectTab(1);
                    if(!installParser)
                    {
                        button28.Visible = true;
                        button6.Visible = false;
                    }
                    else
                    {
                        button28.Visible = false;
                        button6.Visible = true;
                    }
                }
                else
                {
                    string message = "В пути должны отсутствовать пробелы, спецсимволы, знаки табуляции и русские символы.";
                    string caption = "Ошибка";
                    DialogResult result;
                    result = MessageBox.Show(message, caption);
                }

            }
            else
            {
                string message = "Не все поля заполнены.";
                string caption = "Ошибка";
                DialogResult result;
                result = MessageBox.Show(message, caption);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false & !textBox2.Text.Equals(""))
            {
                if (configProxyForLoadDataFromBDAndExecute(textBox2.Text))
                {
                    tabControl1.SelectedIndex = 2;
                }
            }
            else
            {
                if (checkTextBoxesWithDBParam())
                {
                    if (configProxyForLoadDataFromBDAndExecute(textBox2.Text))
                    {
                        tabControl1.SelectedIndex = 2;
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // получаем имя выбранного файла
            textBox2.Text = openFileDialog1.FileName;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // получаем имя выбранного файла
            textBox3.Text = openFileDialog1.FileName;
        }

        private void CopyFile(string sourcefn, string destinfn)
        {
            FileInfo fn = new FileInfo(sourcefn);
            fn.CopyTo(destinfn, true);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = textBox3.Text;
                textBox3.Text = "";
            }
            else
            {
                string message = "Нельзя добавить пустой путь. Для добавления нажмите \"Обзор\", выберите нужный файл, затем нажмите \"Добавить\"";
                string caption = "Ошибка добавления";
                DialogResult result;
                result = MessageBox.Show(message, caption);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if(checkBox2.CheckState == CheckState.Checked)
            {
                textBox2.ReadOnly = true;
                textBox4.Enabled = true;
                textBox6.Enabled = true;
            }
            else
            {
                textBox2.ReadOnly = false;
                textBox4.Enabled = false;
                textBox6.Enabled = false;
                textBox4.Text = "";
                textBox6.Text = "";
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                dateTimePicker2.Visible = false;
            }
            else
            {
                dateTimePicker2.Visible = true;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(numericUpDown1.Value == 0)
            {
                numericUpDown1.Value = 1;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = 59;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = 24;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if ((radioButton4.Checked == true & textBox5.Text!="") | radioButton3.Checked == true)
                try
                {
                    string last_record_s_time = "";
                    string path_of_program;
                    List<string> path_of_log_file = new List<string>();



                    //обновление пути установки
                    path_of_program = textBox1.Text;
                    IniFiles INI = new IniFiles(path_of_program + "\\config.ini");

                    //обновление даты последней записи в бд
                    if (checkBox1.CheckState == CheckState.Unchecked)//если она существует
                    {

                        string date_time = dateTimePicker2.Text;
                        string[] words = date_time.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] date = words[0].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
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
                        last_record_s_time = "12.12.1970_12:12:12";
                    }

                    //создание массива путей к логам
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        path_of_log_file.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    }

                    //копирование файлов в новую директорию
                    CopyFile(Application.StartupPath + "\\ServerKeyLogsParser.exe", path_of_program + "\\ServerKeyLogsParser.exe");


                    //создание файла настроек и файла запуска приложения
                    if(radioButton4.Checked == true)
                    {
                        //buf.Add(textBox5.Text + " PathAvevasParser");
                        INI.Write("Settings", "pathAvevasParser", textBox5.Text);
                    }
                    for (int i = 0; i < path_of_log_file.Count(); i++)
                    {
                        if(i==0)
                        {
                            INI.Write("Settings", "pathOfLogFile", path_of_log_file.ElementAt(0));
                            INI.Write("Settings", "lastDateOfLogFile", last_record_s_time);
                        }
                        else
                        {
                            INI.Write("Settings", "pathOfLogFile" + i.ToString(), 
                                path_of_log_file.ElementAt(i));
                            INI.Write("Settings", "lastDateOfLogFile"
                                    + i.ToString(), last_record_s_time);
                        }
                    }

                    INI.Write("Settings", "connectionString", textBox2.Text);

                    ReadWriteTextFile rwtf = new ReadWriteTextFile();
                    List<string> buf = new List<string>();
                    buf.Add(@"@echo off");
                    buf.Add("cd /d " + path_of_program);
                    buf.Add(path_of_program + "\\ServerKeyLogsParser.exe");
                    rwtf.Write_to_file(buf, path_of_program + "\\RunServerKeyLogsParser.bat", 0);


                    buf.Clear();
                    buf.Add(@"@echo off");
                    buf.Add("cd /d " + textBox5.Text);
                    buf.Add("lsmon aveva > "+ path_of_program + "\\output.txt");
                    rwtf.Write_to_file(buf, path_of_program + "\\CreateAvevasLog.bat", 0);

                    /*ДЛЯ ТЕСТОВ ОТКЛЮЧИЛ
                    //создание задания для планировщика заданий
                    string command;
                    if (radioButton1.Checked == true)
                    {
                        command = @"/C SCHTASKS /Create /SC MINUTE /MO " + numericUpDown1.Value + " /TR " + path_of_program + "\\RunServerKeyLogsParser.bat /TN FlexLMParser";
                    }
                    else
                    {
                        command = @"/C SCHTASKS /Create /SC HOURLY /MO " + numericUpDown1.Value + " /TR " + path_of_program + "\\RunServerKeyLogsParser.bat /TN FlexLMParser";
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
                    }*/


                    tabControl1.SelectTab(6);
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

        private void button23_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            textBox5.Visible = true;
            label25.Visible = true;
            button24.Visible = true;
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            textBox5.Visible = false;
            label25.Visible = false;
            button24.Visible = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            textBox5.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            installParser = true;
            tabControl1.SelectedIndex = 0;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            installParser = false;
            tabControl1.SelectedIndex = 0;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 7;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false & !textBox2.Text.Equals(""))
            {
                configProxyForLoadDataFromBDAndExecute(textBox2.Text);
            }
            else
            {
                if (checkTextBoxesWithDBParam())
                {
                    configProxyForLoadDataFromBDAndExecute(textBox2.Text);
                }
            }
        }

        private bool checkTextBoxesWithDBParam()
        {
            if (!textBox4.Text.Equals(""))
            {
                if (!textBox6.Text.Equals(""))
                {
                    return true;
                }
                else
                {
                    string message = "Не все поля заполнены.";
                    string caption = "Ошибка";
                    DialogResult result;
                    result = MessageBox.Show(message, caption);
                    return false;
                }
            }
            else
            {
                string message = "Не все поля заполнены.";
                string caption = "Ошибка";
                DialogResult result;
                result = MessageBox.Show(message, caption);
                return false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!textBox4.Text.Equals(""))
            {
                if (!textBox6.Text.Equals(""))
                {
                    textBox2.Text = "Provider=" + textBox4.Text + ";Data Source=" +
                        textBox6.Text + ";" +
                        "Integrated Security=SSPI;Initial Catalog=LicenseInformationSystem";
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!textBox4.Text.Equals(""))
            {
                if (!textBox6.Text.Equals(""))
                {
                    textBox2.Text = "Provider=" + textBox4.Text + ";Data Source=" +
                        textBox6.Text + ";" +
                        "Integrated Security=SSPI;Initial Catalog=LicenseInformationSystem";
                }
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //обновление пути установки
            string path_of_program = textBox1.Text;
            //копирование файлов в новую директорию
            IniFiles INI = new IniFiles(path_of_program + "\\config.ini");
            INI.Write("Settings", "connectionString", textBox2.Text);
            CopyFile(Application.StartupPath + "\\Analytics.exe", path_of_program + "\\Analytics.exe");

            tabControl1.SelectedIndex = 6;
        }
    }
}
