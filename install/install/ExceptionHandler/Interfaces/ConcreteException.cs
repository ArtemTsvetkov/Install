﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.ExceptionHandler.Interfaces
{
    interface ConcreteException
    {
        //Обработка исключения, используя данные базового
        void processing(Exception basic);
    }
}
