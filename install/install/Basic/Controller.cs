using install.Interfaces.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using install.Interfaces;
using install.Basic.ModelsParts.Types.TimeTypes;
using install.Basic.ModelsParts.Types.ProgramTypes;

namespace install.Basic
{
    class Controller : ControllerInterface
    {
        private ModelInterface model;

        public Controller(ModelInterface model)
        {
            this.model = model;
        }

        public void initAdmin(SecurityUserInterface admin)
        {
            Config config = new Config();
            config.admin = admin.copy();
            model.updateConfig(config);
        }

        public void initDataBase()
        {
            model.initDataBase();
        }

        public void install()
        {
            model.install();
        }

        public void setAvevasPath(string path)
        {
            Config config = new Config();
            config.avevasPath = path;
            model.updateConfig(config);
        }

        public void setConnectionString(string connection)
        {
            Config config = new Config();
            config.connection = connection;
            model.updateConfig(config);
        }

        public void setInstalledProgramType(ProgramType programType)
        {
            Config config = new Config();
            config.programType = programType;
            model.updateConfig(config);
        }

        public void setLastDate(string date)
        {
            Config config = new Config();
            config.date = date;
            model.updateConfig(config);
        }

        public void setLogsPath(List<string> logs)
        {
            Config config = new Config();
            config.logs = new List<string>();
            for(int i=0; i<logs.Count; i++)
            {
                config.logs.Add(logs.ElementAt(i));
            }
            model.updateConfig(config);
        }

        public void setProgramPath(string path)
        {
            Config config = new Config();
            config.programPath = path;
            model.updateConfig(config);
        }

        public void setTimeModificator(int modificator, TimeType timeType)
        {
            Config config = new Config();
            config.modificator = modificator;
            config.timeType = timeType;
            model.updateConfig(config);
        }
    }
}
