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
        public Form1()
        {
            InitializeComponent();
            //настройка переключателя
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabStop = false;
            radioButton1.Checked = true;
            textBox5.Visible = false;
            label25.Visible = false;
            button24.Visible = false;
            radioButton3.Checked = true;
            //поля ввода пароля бд
            textBox4.Visible = false;
            label17.Visible = false;
            //добавление колонок в таблицу
            DataGridViewTextBoxColumn coefficient0;
            coefficient0 = new DataGridViewTextBoxColumn();
            coefficient0.Width = 363;
            coefficient0.HeaderText = "Пути к логам";
            dataGridView1.Columns.Add(coefficient0);
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
            if (checkBox2.CheckState == CheckState.Checked)
            {
                if (textBox4.Text != "" & textBox2.Text != "" & textBox6.Text != "")
                {
                    tabControl1.SelectTab(2);
                }
                else
                {
                    string message = "Не все поля заполнены.";
                    string caption = "Ошибка";
                    DialogResult result;
                    result = MessageBox.Show(message, caption);
                }
            }
            else
            {
                if (textBox2.Text != "")
                {
                    tabControl1.SelectTab(2);
                }
                else
                {
                    string message = "Не все поля заполнены.";
                    string caption = "Ошибка";
                    DialogResult result;
                    result = MessageBox.Show(message, caption);
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
                textBox4.Visible = true;
                label17.Visible = true;
            }
            else
            {
                textBox4.Visible = false;
                label17.Visible = false;
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
            if((radioButton4.Checked == true & textBox5.Text!="") | radioButton3.Checked == true)
                try
                {
                    string last_record_s_time = "";
                    string path_of_data_base = "";
                    string path_of_program;
                    string password_of_data_base = "";
                    List<string> path_of_log_file = new List<string>();
                    string server_host = "";
                    bool done_to_install = true;//если что-то не получится, программа не будет выполнена до конца, все файлы и настройки системы не изменятся



                    //обновление пути установки
                    path_of_program = textBox1.Text;

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

                    //обновление пути к базе данных
                    path_of_data_base = textBox2.Text;

                    //если есть пароль, то запоминаю
                    if (checkBox2.CheckState == CheckState.Checked)
                    {
                        password_of_data_base = textBox4.Text;
                    }

                    //создание массива путей к логам
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        path_of_log_file.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    }

                    //получение имени сервера
                    WorkWithWindowsCommandLine wwwcl = new WorkWithWindowsCommandLine();
                    string command = @"/C hostname";
                    server_host = wwwcl.Run_command(command);//в переменную server_host записываю значение только чтобы не создавать нувую переменную, здесь просто лежит ответ командной строки
                    if (server_host == "")
                    {
                        string message = "Не удалось получить название сервера.";
                        string caption = "Ошибка";
                        DialogResult result;
                        result = MessageBox.Show(message, caption);
                        done_to_install = false;
                    }
                    else
                    {
                        //удаление лишних символов
                        server_host = server_host.Remove((server_host.Count() - 2), 2);
                    }



                    if (done_to_install != false)
                    {
                        //копирование файлов в новую директорию
                        CopyFile(Application.StartupPath + "\\ServerKeyLogsParser.exe", path_of_program + "\\ServerKeyLogsParser.exe");


                        //создание файла настроек и файла запуска приложения
                        ReadWriteTextFile rwtf = new ReadWriteTextFile();
                        List<string> buf = new List<string>();
                        buf.Add(server_host + " server's_host");
                        //buf.Add(last_record_s_time + " last_record's_time");
                        if (password_of_data_base != "")
                        {
                            buf.Add(password_of_data_base + " password");
                        }
                        buf.Add(path_of_data_base + " path_of_data_base");
                        if(radioButton4.Checked == true)
                        {
                            buf.Add(textBox5.Text + " PathAvevasParser");
                        }
                        for (int i = 0; i < path_of_log_file.Count(); i++)
                        {
                            buf.Add(path_of_log_file.ElementAt(i) + " path_of_log_file " + last_record_s_time);
                        }
                        buf.Add(textBox6.Text + " table");
                        rwtf.Write_to_file(buf, path_of_program + "\\settings.txt", 0);


                        buf.Clear();
                        buf.Add(@"@echo off");
                        buf.Add("cd /d " + path_of_program);
                        buf.Add(path_of_program + "\\ServerKeyLogsParser.exe");
                        rwtf.Write_to_file(buf, path_of_program + "\\RunServerKeyLogsParser.bat", 0);


                        buf.Clear();
                        buf.Add(@"@echo off");
                        buf.Add("cd /d " + textBox5.Text);
                        buf.Add("lsmon aveva > "+ path_of_program + "\\output.txt");
                        rwtf.Write_to_file(buf, path_of_program + "\\CreateAvevasLog.bat", 0);


                        //создание задания для планировщика заданий
                        if (checkBox1.Checked == true)
                        {
                            command = @"/C SCHTASKS /Create /SC MINUTE /MO " + numericUpDown1.Value + " /TR " + path_of_program + "\\RunServerKeyLogsParser.bat /TN FlexLMParser";
                        }
                        else
                        {
                            command = @"/C SCHTASKS /Create /SC HOURLY /MO " + numericUpDown1.Value + " /TR " + path_of_program + "\\RunServerKeyLogsParser.bat /TN FlexLMParser";
                        }
                        server_host = wwwcl.Run_command(command);//в переменную server_host записываю значение только чтобы не создавать нувую переменную, здесь просто лежит ответ командной строки
                        if (server_host == "")
                        {
                            string message = "Не удалось создать задание для планировщика заданий.";
                            string caption = "Ошибка";
                            DialogResult result;
                            result = MessageBox.Show(message, caption);
                        }
                        tabControl1.SelectTab(6);
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
    }
}
