using install.ExceptionHandler.Interfaces;
using install.ExceptionHandler.View.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Exceptions
{
    class FinishChain : Exception, ConcreteException
    {
        public FinishChain() : base() { }

        public FinishChain(string message) : base(message) { }

        public void processing(Exception ex)
        {
            ExceptionViewInterface<ErrorPopupWindowConfig> view = new ErrorPopupWindow();
            ErrorPopupWindowConfig config = new ErrorPopupWindowConfig(
                "Цепочка читателей конфига модели закончилась и не смогла обработать конфиг");
            view.setConfig(config);
            view.show();
        }
    }
}
