﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace install
{
    class ReadWriteTextFile
    {
        public bool Write_to_file(List<String> buf_of_file_lines, string file_path, int action)
        {
            try
            {
                FileMode fm = FileMode.Create;
                if (action == 0)
                {
                    fm = FileMode.Append;
                }
                if (action == 1)
                {
                    fm = FileMode.Create;
                }
                if (action == 2)
                {
                    fm = FileMode.CreateNew;
                }
                if (action == 3)
                {
                    fm = FileMode.Open;
                }
                if (action == 4)
                {
                    fm = FileMode.OpenOrCreate;
                }
                if (action == 5)
                {
                    fm = FileMode.Truncate;
                }
                FileStream file1 = new FileStream(file_path, fm); //создаем файловый поток
                StreamWriter writer = new StreamWriter(file1, Encoding.ASCII); //создаем «потоковый писатель» и связываем его с файловым потоком 
                for (int i = 0; i < buf_of_file_lines.Count; i++)
                {
                    writer.WriteLine(buf_of_file_lines.ElementAt(i)); //записываем в файл
                }
                writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<String> Read_from_file(string file_path)
        {
            List<String> buf_of_file_lines = new List<string>();
            try
            {
                FileStream file1 = new FileStream(file_path, FileMode.Open); //создаем файловый поток
                StreamReader reader = new StreamReader(file1, Encoding.UTF8); // создаем «потоковый читатель» и связываем его с файловым потоком 
                while (reader.EndOfStream == false)
                {
                    buf_of_file_lines.Add(reader.ReadLine()); //считываем все данные с потока и выводим на экран
                }
                reader.Close(); //закрываем поток
                return buf_of_file_lines;
            }
            catch
            {
                buf_of_file_lines.Clear();
                buf_of_file_lines.Add("Ошибка чтения, файл не существует или не доступен!");
                return buf_of_file_lines;
            }
        }
    }
}
/*
 * Модуль для работы с файлами, для корректной работы файлы должны быть в кодировке utf8!
 * При успешной записи в файл функция Write_to_file вернет "true", иначе "false".
 * При успешном чтении из файла функция вернет список строк, иначе вернет список, в котором будет одна строка
 * со значением:"Ошибка чтения, файл не существует или не доступен!".
 * При записи в файл, одним из параметров является пареметр action, который принимает значение от 0 до 5 и задает режим записи и создания файла
 */
