using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Interfaces.Data
{
    interface DataWorker<TStateWithConfigFields, TStorage>
    {
        void setConfig(TStateWithConfigFields fields);
        void execute();
        bool connect();
        TStorage getResult();
    }
}
/*
 * Позволяет работать с данными
 */
