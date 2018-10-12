﻿using install.Exceptions;
using install.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.WorkWithDataBase.MsSqlServer
{
    class MsSQLServerProxy : DataWorker<MsSQLServerStateFields, DataSet>
    {
        private DataWorker<MsSQLServerStateFields, DataSet> saver = new MsSQLServerDataWorker();
        private MsSQLServerStateFields config;

        public void setConfig(MsSQLServerStateFields config)
        {
            this.config = config;
            saver.setConfig(config);
        }

        public void execute()
        {
            if (connect())
            {
                saver.execute();
            }
            else
            {
                throw new NoDataBaseConnection("There is no database connection");
            }
        }

        //если запрос выполнился, значит подключение есть
        public bool connect()
        {
            if (config.getQuery() == null)
            {
                string connStr = string.Format(config.getConnectionString());
                OleDbConnection conn;
                conn = null;

                try
                {
                    conn = new OleDbConnection(connStr);
                    conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new NoDataBaseConnection("There is no database connection");
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            else
            {
                List<string> currentQuerys = new List<string>();
                currentQuerys.Add("SELECT 1 FROM Vendor");
                string connStr = string.Format(config.getConnectionString());
                DataSet dataSet = new DataSet();
                OleDbConnection conn;
                conn = null;

                try
                {
                    conn = new OleDbConnection(connStr);
                    conn.Open();
                    for (int i = 0; i < currentQuerys.Count; i++)
                    {
                        try
                        {
                            OleDbDataAdapter adapter = new OleDbDataAdapter(currentQuerys.ElementAt(i),
                                conn);
                            adapter.Fill(dataSet, selectTableNameFromQuery(currentQuerys.ElementAt(i)));
                        }
                        catch (Exception ex)
                        {
                            throw new DatabaseQueryError("Database query error. Query:" +
                                    currentQuerys.ElementAt(i));
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw new NoDataBaseConnection("There is no database connection");
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
        }

        //функция поиска названия таблицы базы данных
        private string selectTableNameFromQuery(string query)
        {
            string[] buf_of_substrings = query.Split(new char[] { ' ' }, StringSplitOptions.
                RemoveEmptyEntries);
            if (buf_of_substrings[0].Equals("SELECT", StringComparison.CurrentCultureIgnoreCase) ==
                true)
            {
                return buf_of_substrings[3];
            }
            if (buf_of_substrings[0].Equals("INSERT", StringComparison.CurrentCultureIgnoreCase) ==
                true)
            {
                return buf_of_substrings[2];
            }
            if (buf_of_substrings[0].Equals("UPDATE", StringComparison.CurrentCultureIgnoreCase) ==
                true)
            {
                return buf_of_substrings[1];
            }
            if (buf_of_substrings[0].Equals("DELETE", StringComparison.CurrentCultureIgnoreCase) ==
                true)
            {
                return buf_of_substrings[2];
            }
            return null;
        }

        public DataSet getResult()
        {
            return saver.getResult();
        }
    }
}
/*
 * Контролирует возможность доступа к БД. Реализация паттерна "Посредник"
 */
