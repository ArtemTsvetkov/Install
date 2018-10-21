using install.Basic.Model.Chain;
using install.Basic.ModelsParts.Types.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.ModelsParts.Chain.Concrete
{
    class UpdateTimeConfig : BaseConfigReader
    {
        public override Config read(ModelsState modelsState, Config newConfig)
        {
            if (newConfig.timeType != null & newConfig.modificator!=-1)
            {
                modelsState.config.modificator = newConfig.modificator;
                modelsState.config.timeType = newConfig.timeType;
                modelsState.result = new Result(new OkType());
                return modelsState.config;
            }
            else
            {
                return giveNext(modelsState, newConfig);
            }
        }
    }
}