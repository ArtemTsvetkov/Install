using install.Interfaces.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.ModelsParts
{
    class ModelsState
    {
        public List<Observer> observers;
        public Config config;
        public Result result;

        public ModelsState()
        {
            config = new Config();
        }

        public ModelsState copy()
        {
            ModelsState copy = new ModelsState();
            copy.config = config.copy();
            copy.result = result;
            copy.observers = observers;

            return copy;
        }
    }
}
