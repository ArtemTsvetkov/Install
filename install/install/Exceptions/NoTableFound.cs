using install.ExceptionHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Exceptions
{
    class NoTableFound : Exception, ConcreteException
    {
        public NoTableFound() : base() { }

        public NoTableFound(string message) : base(message) { }

        public void processing(Exception ex)
        {

        }
    }
}
