using install.Basic.ModelsParts.Chain;
using install.Basic.ModelsParts.Types.ResultTypes;
using install.Installer;
using install.Interfaces.Basic;
using install.Interfaces.Installer;
using install.WorkWithDataBase.MsSqlServer;
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
            modelsState = new ModelsState();
            modelsState.observers = new List<Observer>();
            installer = new ConcreteInstaller();
        }

        public Result getResult()
        {
            return modelsState.result;
        }

        public void install()
        {
            GeneralInstallator generalInstallator = new GeneralInstallator(installer);
            modelsState = generalInstallator.install(modelsState.copy());
            notifyObservers();
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
            try
            {
                modelsState.config = chainCreator.updateConfig(modelsState, config);
                notifyObservers();
            }
            catch(Exception ex)
            {
                ExceptionHandler.Concrete.ExceptionHandler.getInstance().processing(ex);
            }
        }

        public void initDataBase()
        {
            installer.createDatabaseTables(modelsState.config.connection);
            installer.creatAdmin(modelsState.config.connection, 
                modelsState.config.admin.getLogin(), modelsState.config.admin.getPassword());
            modelsState.result = new Result(new OkType());
            notifyObservers();
        }
    }
}
