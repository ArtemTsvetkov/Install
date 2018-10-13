using install.Interfaces.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.ModelsParts
{
    class Model : ModelInterface
    {
        private List<Observer> observers;
        private Config config;

        public Result getResult()
        {
            throw new NotImplementedException();
        }

        public void install()
        {
            throw new NotImplementedException();
        }

        public void notifyObservers()
        {
            throw new NotImplementedException();
        }

        public void setConfig(Config config)
        {
            throw new NotImplementedException();
        }

        public void subscribe(Observer observer)
        {
            throw new NotImplementedException();
        }

        public void updateConfig(Config config)
        {
            throw new NotImplementedException();
        }
    }
}
