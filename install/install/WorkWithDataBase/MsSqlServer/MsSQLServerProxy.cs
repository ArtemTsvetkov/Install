using install.Exceptions;
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
            try
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
            catch (Exception ex)
            {
                ExceptionHandler.Concrete.ExceptionHandler.getInstance().processing(ex);
            }
        }

        //если запрос выполнился, значит подключение есть
        public bool connect()
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
        
        public DataSet getResult()
        {
            return saver.getResult();
        }
    }
}
/*
 * Контролирует возможность доступа к БД. Реализация паттерна "Посредник"
 */
