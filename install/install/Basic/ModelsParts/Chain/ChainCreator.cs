using install.Basic.Model.Chain;
using install.Basic.ModelsParts.Chain.Concrete;
using install.WorkWithDataBase.MsSqlServer;
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

        public ChainCreator()
        {
            init();
        }

        public void init()
        {
            UpdateTimeConfig updateTimeConfig = new UpdateTimeConfig();
            ProgramTypeConfig programTypeConfig = new ProgramTypeConfig();
            programTypeConfig.setNext(updateTimeConfig);
            ProgramPathConfig programPathConfig = new ProgramPathConfig();
            programPathConfig.setNext(programTypeConfig);
            LogsConfig logsConfig = new LogsConfig();
            logsConfig.setNext(programPathConfig);
            DateConfig dateConfig = new DateConfig();
            dateConfig.setNext(logsConfig);
            DataBaseConfig dataBaseConfig = new DataBaseConfig();
            dataBaseConfig.setNext(dateConfig);
            ConnectionConfig connectionConfig = new ConnectionConfig(new MsSqlServerController());
            connectionConfig.setNext(dataBaseConfig);
            firstConfigReader = new AvevaConfig();
            firstConfigReader.setNext(connectionConfig);
        }
        public Config updateConfig(ModelsState modelsState, Config newConfig)
        {
            return firstConfigReader.read(modelsState, newConfig);
        }
    }
}
