using install.ExceptionHandler.Interfaces;
using install.ExceptionHandler.View.Information.PopupWindow;
using install.Exceptions;
using install.Interfaces.Data;
using install.Interfaces.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.WorkWithDataBase.MsSqlServer
{
    class MsSqlServerController : DataBaseControllerInterface
    {
        public bool configAndCheckConnect(string connectionString)
        {
            DataWorker<MsSQLServerStateFields, DataSet> accessProxy =
                setConfigToProxyDataBase(connectionString);
            try
            {
                accessProxy.connect();
                ExceptionViewInterface<InformationPopupWindowConfig> view =
                    new InformationPopupWindow();
                InformationPopupWindowConfig config = new InformationPopupWindowConfig(
                    "Соединение установлено");
                view.setConfig(config);
                view.show();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Concrete.ExceptionHandler.getInstance().processing(
                    new NoDataBaseConnection());
                return false;
            }
        }

        public bool configAndExecute(string connectionString, string query)
        {
            DataWorker<MsSQLServerStateFields, DataSet> accessProxy =
                setConfigToProxyDataBase(connectionString, query);
            try
            {
                accessProxy.execute();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Concrete.ExceptionHandler.getInstance().processing(ex);
                return false;
            }
        }

        private DataWorker<MsSQLServerStateFields, DataSet> setConfigToProxyDataBase(
            string connectionString, string query)
        {
            DataWorker<MsSQLServerStateFields, DataSet> accessProxy = new MsSQLServerProxy();
            List<string> list = new List<string>();
            list.Add(query);
            MsSQLServerStateFields configProxy =
                new MsSQLServerStateFields(connectionString, list);
            accessProxy.setConfig(configProxy);

            return accessProxy;
        }

        private DataWorker<MsSQLServerStateFields, DataSet> setConfigToProxyDataBase(
            string connectionString)
        {
            DataWorker<MsSQLServerStateFields, DataSet> accessProxy = new MsSQLServerProxy();
            MsSQLServerStateFields configProxy =
                new MsSQLServerStateFields(connectionString);
            accessProxy.setConfig(configProxy);

            return accessProxy;
        }
    }
}
