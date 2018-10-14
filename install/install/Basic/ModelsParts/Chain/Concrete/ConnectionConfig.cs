using install.Basic.Model.Chain;
using install.Basic.ModelsParts.Types.ResultTypes;
using install.Interfaces.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace install.Basic.ModelsParts.Chain.Concrete
{
    class ConnectionConfig : BaseConfigReader
    {
        private DataBaseControllerInterface controller;

        public ConnectionConfig(DataBaseControllerInterface controller)
        {
            this.controller = controller;
        }

        public override Config read(ModelsState modelsState, Config newConfig)
        {
            if (newConfig.connection != null)
            {
                if (controller.configAndCheckConnect(newConfig.connection))
                {
                    modelsState.config.connection = newConfig.connection;
                    modelsState.result = new Result(new ConnectionReadyType());
                    return modelsState.config;
                }
                else
                {
                    modelsState.result = new Result(new ConnectionNotReadyType());
                    return modelsState.config;
                }
            }
            else
            {
                return giveNext(modelsState, newConfig);
            }
        }
    }
}
