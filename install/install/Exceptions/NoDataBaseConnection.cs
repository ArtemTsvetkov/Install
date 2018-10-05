using install.ExceptionHandler.Interfaces;
using install.ExceptionHandler.View.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Exceptions
{
    class NoDataBaseConnection : Exception, ConcreteException
    {
        public NoDataBaseConnection() : base() { }

        public NoDataBaseConnection(string message) : base(message) { }

        public void processing(Exception ex)
        {
            ExceptionViewInterface<ErrorPopupWindowConfig> view = new ErrorPopupWindow();
            ErrorPopupWindowConfig config = new ErrorPopupWindowConfig(
                "Нет соединения с базой данных! Проверьте параметры подключения к серверу БД, "+
                "а так же наличие на сервере пустой БД с названием "+
                "<LicenseInformationSystem>, если она отсутствует-создайте ее.");
            view.setConfig(config);
            view.show();
        }
    }
}
