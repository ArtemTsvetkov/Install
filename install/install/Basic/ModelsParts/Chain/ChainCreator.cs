using install.Basic.Model.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.ModelsParts.Chain
{
    class ChainCreator
    {
        private ConfigReader firstConfigReader;

        public void init()
        {

        }
        public Config updateConfig(Config currentConfig, Config newConfig)
        {
            return firstConfigReader.read(currentConfig, newConfig);
        }
    }
}
