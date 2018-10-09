using install.ExceptionHandler.Interfaces;
using install.ExceptionHandler.View.Information.PopupWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.ExceptionHandler.Exceptions
{
    class NonFoundException : Exception, ConcreteException
    {
        public NonFoundException() : base() { }

        public NonFoundException(string message) : base(message) { }

        public void processing(Exception ex)
        {
            ExceptionViewInterface<InformationPopupWindowConfig> view = new InformationPopupWindow();
            InformationPopupWindowConfig config = new InformationPopupWindowConfig(
                "Неизвестное исключение " + ex.GetType());
            view.setConfig(config);
            view.show();
        }
    }
}

