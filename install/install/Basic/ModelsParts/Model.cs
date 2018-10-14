using install.Basic.ModelsParts.Chain;
using install.Installer;
using install.Interfaces.Basic;
using install.Interfaces.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.ModelsParts
{
    class Model : ModelInterface
    {
        private ModelsState modelsState;
        private InstallerInterface installer;

        public Model()
        {
            modelsState.observers = new List<Observer>();
            installer = new ConcreteInstaller();
        }

        public Result getResult()
        {
            return modelsState.result;
        }

        public void install()
        {
            if(modelsState.config.programType.getType().Equals("AnalitycsType"))
            {
                installer.installAnalytics(modelsState.config.programPath,
                    modelsState.config.connection);
            }
            else
            {
                Director installDirector = new Director(new ConcreteIntallBuilder());
                installDirector.install(modelsState.config.copy());
            }
        }

        public void notifyObservers()
        {
            for (int i = 0; i < modelsState.observers.Count; i++)
            {
                modelsState.observers.ElementAt(i).notify();
            }
        }

        public void setConfig(Config config)
        {
            modelsState.config = config.copy();
        }

        public void subscribe(Observer observer)
        {
            modelsState.observers.Add(observer);
        }

        public void updateConfig(Config config)
        {
            ChainCreator chainCreator = new ChainCreator();
            chainCreator.init();
            try
            {
                modelsState.config = chainCreator.updateConfig(modelsState.config, config);
            }
            catch(Exception ex)
            {
                ExceptionHandler.Concrete.ExceptionHandler.getInstance().processing(ex);
            }
        }
    }
}
