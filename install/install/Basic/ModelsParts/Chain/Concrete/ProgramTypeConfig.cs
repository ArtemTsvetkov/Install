using install.Basic.Model.Chain;
using install.Basic.ModelsParts.Types.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.ModelsParts.Chain.Concrete
{
    class ProgramTypeConfig : BaseConfigReader
    {
        public override Config read(ModelsState modelsState, Config newConfig)
        {
            if (newConfig.programType != null)
            {
                modelsState.config.programType = newConfig.programType;
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
