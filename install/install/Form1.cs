using install.Basic;
using install.Basic.ModelsParts;
using install.Basic.ModelsParts.Types.ProgramTypes;
using install.Basic.ModelsParts.Types.TimeTypes;
using install.ExceptionHandler.Concrete;
using install.ExceptionHandler.Interfaces;
using install.ExceptionHandler.View.Information.PopupWindow;
using install.Exceptions;
using install.Hash;
using install.IniComponent;
using install.Interfaces;
using install.Interfaces.Basic;
using install.Interfaces.Data;
using install.Interfaces.DataBase;
using install.Interfaces.QueryConfigurator;
using install.QueryConfigurator;
using install.Security;
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
        private ControllerInterface controller;

        public Form1()
        {
            InitializeComponent();
            //
            //Exception handler
            //
            ConcreteExceptionHandlerInitializer.initThisExceptionHandler(
                ExceptionHandler.Concrete.ExceptionHandler.getInstance());
            //
            //MVC
            //
            ModelInterface model = new Model();
            Basic.View view = new Basic.View(this, model);
            controller = new Controller(model);
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
            button6.Enabled = false;
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
            controller.setProgramPath(folderBrowserDialog1.SelectedPath);
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
                    showErrorMessage("В пути должны отсутствовать пробелы, спецсимволы,"+
                        " знаки табуляции и русские символы.");
                }

            }
            else
            {
                showErrorMessage("Не все поля заполнены.");
            }
        }

        private void showErrorMessage(string error)
        {
            string message = error;
            string caption = "Ошибка";
            DialogResult result;
            result = MessageBox.Show(message, caption);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
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
            if (checkBox1.CheckState == CheckState.Unchecked)//если она существует
            {
                controller.setLastDate(dateTimePicker2.Text);
            }


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
                showErrorMessage("Нельзя добавить пустой путь. Для добавления "+
                    "нажмите \"Обзор\", выберите нужный файл, затем нажмите \"Добавить\"");
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
            if (radioButton1.Checked == true)
            {
                controller.setTimeModidicatorType(new MinuteType());
            }
            else
            {
                controller.setTimeModidicatorType(new HourType());
            }
            controller.setTimeModificator(int.Parse(numericUpDown1.Value.ToString()));

            tabControl1.SelectTab(5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> logs = new List<string>();
            //создание массива путей к логам
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                logs.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
            controller.setLogsPath(logs);
            
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
            if ((radioButton4.Checked == true & textBox5.Text != "") | radioButton3.Checked == true)
            {
                controller.install();
            }
            else
            {
                showErrorMessage("Пожалуйста, введите путь для утилиты lsmon.");
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
            controller.setAvevasPath(folderBrowserDialog1.SelectedPath);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            controller.setInstalledProgramType(new ParserType());
            tabControl1.SelectedIndex = 0;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            controller.setInstalledProgramType(new AnalitycsType());
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
                controller.setConnectionString(textBox2.Text);
            }
            else
            {
                if (checkTextBoxesWithDBParam())
                {
                    controller.setConnectionString(textBox2.Text);
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
                    showErrorMessage("Не все поля заполнены.");
                    return false;
                }
            }
            else
            {
                showErrorMessage("Не все поля заполнены.");
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

        private void button31_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (!textBox8.Text.Equals("") & !textBox7.Text.Equals(""))
            {
                if (textBox8.Text.Equals(textBox9.Text))
                {
                    SecurityUserInterface admin = new SecurityUser(textBox7.Text, textBox8.Text);
                    admin.setAdmin(true);
                    controller.initDataBase(admin);
                    controller.install();
                    tabControl1.SelectedIndex = 6;
                }
                else
                {
                    string message = "Пароли не совпадают";
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

        private void button30_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
