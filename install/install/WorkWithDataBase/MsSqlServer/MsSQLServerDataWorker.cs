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
    class MsSQLServerDataWorker : DataWorker<MsSQLServerStateFields, DataSet>
    {
        private MsSQLServerStateFields config;
        private DataSet resultStorage;

        public void execute()
        {
            string connStr = string.Format(config.getConnectionString());
            resultStorage = runQuery(connStr);
        }

        //Тестирует возможность подключения к БД, в этом классе(он не прокси) всегда есть
        //подключение
        public bool connect()
        {
            return true;
        }

        public void setConfig(MsSQLServerStateFields config)
        {
            this.config = config;
        }

        private DataSet runQuery(string connection_string)
        {
            //Подключение к БД используется только для проверки параметров подключения
            return null;
        }

        public DataSet getResult()
        {
            return resultStorage;
        }
    }
}
