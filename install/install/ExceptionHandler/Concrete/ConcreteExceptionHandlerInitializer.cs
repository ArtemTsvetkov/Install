using install.ExceptionHandler.Interfaces;
using install.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.ExceptionHandler.Concrete
{
    class ConcreteExceptionHandlerInitializer
    {
        public static void initThisExceptionHandler(ExceptionHandlerInterface handler)
        {
            try
            {
                handler.addException(new NoConfigurationSpecified());
                handler.addException(new NoDataBaseConnection());
                handler.addException(new DatabaseQueryError());
                handler.addException(new NoTableFound());
                handler.addException(new FinishChain());
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }
    }
}
