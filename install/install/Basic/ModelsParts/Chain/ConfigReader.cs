using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.Model.Chain
{
    interface ConfigReader
    {
        Config read(Config modelConfig, Config newConfig);
        void setNext(ConfigReader next);
        Config giveNext(Config modelConfig, Config newConfig);
    }
}
