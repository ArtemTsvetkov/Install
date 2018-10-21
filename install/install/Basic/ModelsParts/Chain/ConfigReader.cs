using install.Basic.ModelsParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.Model.Chain
{
    interface ConfigReader
    {
        Config read(ModelsState modelsState, Config newConfig);
        void setNext(ConfigReader next);
        Config giveNext(ModelsState modelsState, Config newConfig);
    }
}
