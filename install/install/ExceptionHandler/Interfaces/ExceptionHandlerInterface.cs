using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.ExceptionHandler.Interfaces
{
    interface ExceptionHandlerInterface
    {
        //Обработка исключения
        void processing(Exception exception);
        //Добавление нового исключения
        void addException(ConcreteException exception);
    }
}
