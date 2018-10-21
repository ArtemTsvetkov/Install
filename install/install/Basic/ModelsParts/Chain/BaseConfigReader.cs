using install.Basic.ModelsParts;
using install.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.Model.Chain
{
    abstract class BaseConfigReader : ConfigReader
    {
        private ConfigReader next;

        public Config giveNext(ModelsState modelsState, Config newConfig)
        {
            if(next != null)
            {
                return next.read(modelsState, newConfig);
            }
            else
            {
                throw new FinishChain();
            }
        }

        abstract public Config read(ModelsState modelsState, Config newConfig);

        public void setNext(ConfigReader next)
        {
            this.next = next;
        }
    }
}
