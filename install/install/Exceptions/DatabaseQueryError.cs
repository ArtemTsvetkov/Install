using install.ExceptionHandler.Interfaces;
using install.ExceptionHandler.View.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Exceptions
{
    class DatabaseQueryError : Exception, ConcreteException
    {
        public DatabaseQueryError() : base() { }

        public DatabaseQueryError(string message) : base(message) { }

        public void processing(Exception ex)
        {
            ExceptionViewInterface<ErrorPopupWindowConfig> view = new ErrorPopupWindow();
            ErrorPopupWindowConfig config = new ErrorPopupWindowConfig(
                "Синтаксическая ошибка в запросе к БД");
            view.setConfig(config);
            view.show();
        }
    }
}
