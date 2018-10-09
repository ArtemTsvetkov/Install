using install.ExceptionHandler.Interfaces;
using install.ExceptionHandler.View.Information.PopupWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Exceptions
{
    class NoConfigurationSpecified : Exception, ConcreteException
    {
        public NoConfigurationSpecified() : base() { }

        public NoConfigurationSpecified(string message) : base(message) { }

        public void processing(Exception ex)
        {
            ExceptionViewInterface<InformationPopupWindowConfig> view = new InformationPopupWindow();
            InformationPopupWindowConfig config = new InformationPopupWindowConfig(
                "Конфигурация не задана");
            view.setConfig(config);
            view.show();
        }
    }
}
